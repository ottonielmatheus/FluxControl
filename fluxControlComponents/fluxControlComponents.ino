#define GREENLED 9
#define REDLED 8

struct Status
{
  unsigned short fromStatusText(String statusText)
  {
    short separator = statusText.indexOf(":");
    String _statusText = statusText.substring(1, separator);
    
    if (_statusText.equals("100"))
      return 100;

    else if (_statusText.equals("200"))
      return 200;

    else if (_statusText.equals("201"))
      return 201;

    else if (_statusText.equals("404"))
      return 404;

    else if (_statusText.equals("405"))
      return 405;

    else if (_statusText.equals("500"))
      return 500;

    return 0;
  }
  
  String fromStatusCode(unsigned short statusCode, String data = "")
  {
    switch(statusCode)
    {
      case 100:
        return "<100:DATA>" + data + "<100:DATA/>";

      case 200:
        return "<200:OK/>";
      
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

  void SendStatusCode(unsigned short statusCode, String data = "")
  {
    Serial.read();
    Serial.print(fromStatusCode(statusCode, data));
  }
};

Status STATUS;

void setup() {

  pinMode(GREENLED, OUTPUT);
  pinMode(REDLED, OUTPUT);
  
  Serial.begin(9600);

  // Acende os dois leds para mostrar a iniciação do sistema
  digitalWrite(GREENLED, HIGH);
  digitalWrite(REDLED, HIGH);
  
  delay(3 * 1000);

  digitalWrite(GREENLED, LOW);
  digitalWrite(REDLED, LOW);
   
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
