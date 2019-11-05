#include <WiFi.h>
#include <HTTPClient.h>

// CAM
#include "esp_camera.h"
#include "esp_timer.h"
#include "img_converters.h"
#include "Arduino.h"
#include "fb_gfx.h"
#include "fd_forward.h"
#include "fr_forward.h"
#include "soc/soc.h"           // Disable brownour problems
#include "soc/rtc_cntl_reg.h"  // Disable brownour problems
#include "driver/rtc_io.h"

#define FLASH 4

// Pin definition for CAMERA_MODEL_AI_THINKER
#define PWDN_GPIO_NUM     32
#define RESET_GPIO_NUM    -1
#define XCLK_GPIO_NUM      0
#define SIOD_GPIO_NUM     26
#define SIOC_GPIO_NUM     27

#define Y9_GPIO_NUM       35
#define Y8_GPIO_NUM       34
#define Y7_GPIO_NUM       39
#define Y6_GPIO_NUM       36
#define Y5_GPIO_NUM       21
#define Y4_GPIO_NUM       19
#define Y3_GPIO_NUM       18
#define Y2_GPIO_NUM        5
#define VSYNC_GPIO_NUM    25
#define HREF_GPIO_NUM     23
#define PCLK_GPIO_NUM     22

// WIFI
const String host = "http://192.168.0.14:53114";
 
const char* ssid = "Souza";
const char* password =  "10041974";

const String user_registration = "0";
const String user_password = "!system@emurb!";

String TOKEN = "";

HTTPClient http;

void setup() 
{
  WRITE_PERI_REG(RTC_CNTL_BROWN_OUT_REG, 0); //disable brownout detector
  Serial.begin(115200);

  // ---------------------------- CAM ---------------------------------
  camera_config_t config;
  config.ledc_channel = LEDC_CHANNEL_0;
  config.ledc_timer = LEDC_TIMER_0;
  config.pin_d0 = Y2_GPIO_NUM;
  config.pin_d1 = Y3_GPIO_NUM;
  config.pin_d2 = Y4_GPIO_NUM;
  config.pin_d3 = Y5_GPIO_NUM;
  config.pin_d4 = Y6_GPIO_NUM;
  config.pin_d5 = Y7_GPIO_NUM;
  config.pin_d6 = Y8_GPIO_NUM;
  config.pin_d7 = Y9_GPIO_NUM;
  config.pin_xclk = XCLK_GPIO_NUM;
  config.pin_pclk = PCLK_GPIO_NUM;
  config.pin_vsync = VSYNC_GPIO_NUM;
  config.pin_href = HREF_GPIO_NUM;
  config.pin_sscb_sda = SIOD_GPIO_NUM;
  config.pin_sscb_scl = SIOC_GPIO_NUM;
  config.pin_pwdn = PWDN_GPIO_NUM;
  config.pin_reset = RESET_GPIO_NUM;
  config.xclk_freq_hz = 20000000;
  config.pixel_format = PIXFORMAT_JPEG; 
  
  if(psramFound())
  {
    config.frame_size = FRAMESIZE_UXGA; // FRAMESIZE_ + QVGA|CIF|VGA|SVGA|XGA|SXGA|UXGA
    config.jpeg_quality = 10;
    config.fb_count = 2;
  } 
  else 
  {
    config.frame_size = FRAMESIZE_SVGA;
    config.jpeg_quality = 12;
    config.fb_count = 1;
  }
  
  // Init Camera
  esp_err_t err = esp_camera_init(&config);
  
  if (err != ESP_OK) 
  {
    Serial.printf("Camera init failed with error 0x%x", err);
    return;
  }
  
  // Turns off the ESP32-CAM white on-board LED (flash) connected to GPIO 4
  pinMode(FLASH, OUTPUT);

  // ---------------------------- WIFI ---------------------------------
  delay(4000);   //Delay needed before calling the WiFi.begin
 
  WiFi.begin(ssid, password); 

 //Check for the connection
  while (WiFi.status() != WL_CONNECTED) 
  { 
    delay(1000);
    Serial.println("Connecting to WiFi..");
  }
 
  Serial.println("Connected to the WiFi network");

  //TOKEN = makeLogin();
 
}
 
void loop() 
{
  //Check WiFi connection status
  if(WiFi.status() == WL_CONNECTED)
  {
    
    camera_fb_t * fb = CAPTURE();
   
    if(!fb)
    {
      Serial.println("[X] Erro ao capturar imagem.");
    }
    else
    {
       http.begin(host.concat("/API/FlowRecord/ProcessImageBytes"));  //Specify destination for HTTP request
       http.addHeader("Content-Type", "image/jpeg");
       http.addHeader("Content-Disposition", "form-data; name=\"picture\"; filename=\"cam.jpg\"");
       http.addHeader("Content-Transfer-Encoding", "binary");
       
       int httpResponseCode = http.POST(fb);   //Send the actual POST request
     
       if(httpResponseCode > 0)
       {
          String response = http.getString(); //Get the response to the request
       
          Serial.println(httpResponseCode);   //Print return code
          Serial.println(response);           //Print request answer
       }
       
       else
       {
          Serial.print("[X] Error on sending POST: ");
          Serial.println(httpResponseCode);
       }
   
       http.end(); //Free resources
    }
  }
  else
  {
    Serial.println("[X] Error in WiFi connection");    
  }

  delay(1000 * 10);
}

camera_fb_t* CAPTURE()
{
  // Take Picture with Camera
  digitalWrite(FLASH, HIGH);
  fb = esp_camera_fb_get();
  digitalWrite(FLASH, LOW);

  esp_camera_fb_return(fb);
  rtc_gpio_hold_en(GPIO_NUM_4);

  return fb;
}
