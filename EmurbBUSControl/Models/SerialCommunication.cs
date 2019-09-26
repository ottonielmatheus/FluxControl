﻿using EmurbBUSControl.Hubs;
using EmurbBUSControl.Models;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;

namespace ArduinoCommunication.Models
{
    static public class SerialCommunication
    {
        public enum CommunicationStatusType
        {
            DATA = 100,
            OK = 200,
            RECEIVED = 201,
            NOTRECOGNIZED = 404
        };
        
        public static SerialPort SerialPort = new SerialPort();
        private static string bufferContent = string.Empty;

        public static void Start()
        {
            SerialPort = new SerialPort("COM3")
            {
                BaudRate = 9600,
                Parity = Parity.None,
                StopBits = StopBits.One,
                DataBits = 8,
                Handshake = Handshake.None
            };

            // Subscribe to the DataReceived event.
            SerialPort.DataReceived += SerialPortDataReceived;

            SerialPort.Open();
        }

        public static void SendStatus(CommunicationStatusType status, string data = "")
        {
            try
            {
                switch(status)
                {
                    case CommunicationStatusType.DATA:
                        SerialPort.Write("<100:DATA>" + data + "<100:DATA/>");
                        break;

                    case CommunicationStatusType.OK:
                        SerialPort.Write("<200:OK/>");
                        break;

                    case CommunicationStatusType.NOTRECOGNIZED:
                        SerialPort.Write("<404:NOTRECOGNIZED/>");
                        break;

                    default:
                        SerialPort.Write("<500:SERVERERROR/>");
                        break;
                }
                
            }
            catch(Exception ex)
            {
                Close();
                Start();

                throw ex;
            }
                
        }

        private static void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var serialPort = (SerialPort) sender;

            var serialData = serialPort.ReadExisting();

            // Serial Data
            bufferContent += serialData;

            CommunicationStatus communicationStatus = ConvertToCommuncationStatus(bufferContent);

            if (communicationStatus != null && communicationStatus.StatusCode == CommunicationStatusType.DATA)
            {
                // Registra a entrada no sistema
                SystemNotifier.SendNotificationAsync(communicationStatus);
            }

        }

        public static void Close()
        {
            SerialPort.Close();
        }

        private static CommunicationStatus ConvertToCommuncationStatus(string bufferData)
        {
            CommunicationStatus _cs = new CommunicationStatus();

            if (bufferData.StartsWith("<") && bufferData.EndsWith("/>"))
            {
                var codeSeparator = bufferContent.IndexOf(":");

                // Obtem Status Code na String recebida
                _cs.StatusCode = (CommunicationStatusType)Convert.ToInt16(bufferData.Substring(1, codeSeparator - 1));

                // Caso o Status recebido seja do tipo DATA, capturamos o dado recebido
                if (_cs.StatusCode == CommunicationStatusType.DATA)
                {
                    var contentStartSeparator = bufferData.IndexOf(">");
                    var contentEndSeparator = bufferData.LastIndexOf("<");

                    try
                    {
                        _cs.Data = bufferData.Substring(contentStartSeparator + 1, contentEndSeparator - contentStartSeparator - 1);
                    }
                    catch
                    {
                        _cs.Data = null;
                    }
                }

                // Se a mensagem for reconhecida, limpamos o buffer
                bufferContent = string.Empty;
                return _cs;
            }

            else if (bufferContent.Contains("/0"))
                bufferContent = string.Empty;

            return null;
        }
    }
    
}