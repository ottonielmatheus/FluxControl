#define GREENLED 8
#define REDLED 9

struct Status
{
  unsigned short fromStatusText(String statusText)
  {
    short $separator = statusText.indexOf(":");
    String _statusText = statusText.substring(1, $separator - 1);
    
    if (_status.Text.equals("100"))
      return 100;

    else if (_status.Text.equals("201"))
      return 201;

    else if (_status.Text.equals("404"))
      return 404;

    else if (_status.Text.equals("405"))
      return 405;

    else if (_status.Text.equals("500"))
      return 500;
  }
  
  String fromStatusCode(unsigned short statusCode, String data = NULL)
  {
    switch(statusCode)
    {
      case 100:
        return "<100:DATA>" + data + "<100:DATA/>";
      case 201:
        return "<201:RECEIVED/>";

      case 404:
        return "<404:NOTHINGPROCESSED/>";

      case 405:
        return "<405:NOTRECOGNIZED/>";

      case 500:
        return "<500:SERVERERROR/>";
    }
  }

  void SendStatusCode(unsigned short statusCode, String data = NULL)
  {
    String _statusText = fromStatusCode(statusCode, data);
    Serial.write(_statusText);
  }
};

Status STATUS;

void setup() {

  pinMode(GREENLED, OUTPUT);
  pinMode(REDLED, OUTPUT);
  
  Serial.Begin(9600);

  // Acende os dois leds para mostrar a iniciação do sistema
  digitalWrite(GREENLED, HIGH);
  digitalWrite(REDLED, HIGH);
  
  delay(3 * 1000);
   
}

void loop() {
  
  if (Serial.available())
  {
    unsigned short _status = STATUS.fromStatusText(Serial.readString());

    switch(_status)
    {
      case 200:
        digitalWrite(GREENLED, HIGH);
        STATUS.SendStatusCode(201);
        break;
  
      case 404:
        digitalWrite(REDLED, HIGH);
        STATUS.SendStatusCode(100, "ABC-1234");
        break;
  
      default:
        digitalWrite(REDLED, HIGH);
        STATUS.SendStatusCode(404);
    }

    delay(1000);
    
    digitalWrite(GREENLED, LOW);
    digitalWrite(REDLED, LOW);
  }
}
