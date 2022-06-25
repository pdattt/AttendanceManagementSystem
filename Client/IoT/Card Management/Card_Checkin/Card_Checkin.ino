// References
/*
NTPClient Time: https://randomnerdtutorials.com/esp8266-nodemcu-date-time-ntp-client-server-arduino/
*/

#include "SPI.h"
#include "MFRC522.h"
#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <NTPClient.h>
#include <WiFiUdp.h>
#include <WiFiClient.h>

WiFiClient wifiClient;

const char* ssid = "The Coffee House";
const char* password = "thecoffeehouse";
const String location = "P.001";

const int pinRST = D4;
const int pinSDA = D8;

HTTPClient http;
String url = "http://ams.somee.com/api/session/check-in";
String cardID;

// Define NTP Client to get time
WiFiUDP ntpUDP;
NTPClient timeClient(ntpUDP, "pool.ntp.org");
int currentHour = 0, currentMinute = 0, currentSecond = 0;
int monthDay = 0, currentMonth = 0, currentYear = 0;

MFRC522 mfrc522(pinSDA, pinRST); // Set up mfrc522 on the Arduino

String serverName = "http://192.168.1.106:1880/update-sensor";

void setup() {
  SPI.begin(); // open SPI connection
  mfrc522.PCD_Init(); // Initialize Proximity Coupling Device (PCD)
  Serial.begin(115200); // open serial connection
  connectToWifi();
  
  timeClient.begin();
  timeClient.setTimeOffset(25200);

  Serial.println("Location: " + location);
  Serial.println("------------------------------------------------------");
  delay(1000);
}

void connectToWifi(){
  Serial.printf("Connecting to %s \n", ssid);
  WiFi.begin(ssid, password);
  
   while (WiFi.status() != WL_CONNECTED)
  {
    delay(500);
    Serial.print(".");
  }
  
  Serial.println();
  Serial.print("Connected, IP address: ");
  Serial.println(WiFi.localIP());
}

bool scanningTag(){
  if (mfrc522.PICC_IsNewCardPresent()) { // (true, if RFID tag/card is present ) PICC = Proximity Integrated Circuit Card
    if(mfrc522.PICC_ReadCardSerial()) { // true, if RFID tag/card was read
      //Serial.print("RFID TAG ID:");
      for (byte i = 0; i < mfrc522.uid.size; ++i) { // read id (in parts)
        cardID += String(mfrc522.uid.uidByte[i], HEX); // print id
      }
       // Print out of id is complete.
      Serial.println("Card ID: " + cardID);
    }
    return true;
  }
  return false;
}

void getTime(){
  time_t epochTime = timeClient.getEpochTime();

  currentHour = timeClient.getHours();
  currentMinute = timeClient.getMinutes();
  currentSecond = timeClient.getSeconds();
  
  struct tm *ptm = gmtime ((time_t *)&epochTime); 

  monthDay = ptm->tm_mday;
  currentMonth = ptm->tm_mon+1;
  currentYear = ptm->tm_year+1900;

  Serial.println("Time: " + (String)currentHour + ":" + (String)currentMinute + ":" + (String)currentSecond);
  Serial.println("Date: " + (String)monthDay + "-" + (String)currentMonth + "-" + (String)currentYear);
}

void loop() {
  timeClient.update();
  if(scanningTag()){
    if((WiFi.status() == WL_CONNECTED)){
      url +="?cardId=" + cardID + "&location=" + location;
      http.begin(wifiClient, url);

      int httpCode = http.GET();

      if(httpCode == 200){
        //Success
      }
      else{
        //failed
      }
      
      getTime();
      Serial.println("Location: " + location);
      Serial.println("------------------------------------------------------");
      delay(5000);
    }
  }
}
