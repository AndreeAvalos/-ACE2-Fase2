#include <SoftwareSerial.h>
#include <WiFiEsp.h>
#include <ArduinoJson.h>


//char ssid[] = "Tech_D0016808";
char ssid[] = "";
char pass[] = "";
int status = WL_IDLE_STATUS;
char server[] = "practica1-ace2.azurewebsites.net";
SoftwareSerial BT1(19, 18); // RX | TX
 SoftwareSerial blueSerial(17,16);
WiFiEspServer servidor(443);
//WiFiEspClient clienteServidor;
void setup() {
  // initialize both serial ports:
  Serial.begin(115200);
  Serial1.begin(9600);
  blueSerial.begin(9600);
  //Serial1.begin(115200);
  WiFi.init(&Serial1);
  while (status != WL_CONNECTED)
    {
        Serial.print("Conecting to wifi network: ");
        Serial.println(ssid);

        status = WiFi.begin(ssid, pass);
    }
  Serial.print("IP Address of ESP8266 Module is: ");
  Serial.println(WiFi.localIP());
  Serial.println("You're connected to the network");

  // Start the server
  servidor.begin();
}
WiFiEspClient clienteServidor;
void loop() {
  int adc_MQ7 = analogRead(A0); //Lemos la salida analógica del MQ
  int adc_MQ135 = analogRead(A1);
  int radiacion = analogRead(A2);
  String Tipo="\"Tipo\": ";
  String TipoMedida;
  String carnet="\"carnet\": 201404423,\n";
  String latitud="\"Latitud\": \"14.628011\",\n";
  String longitud="\"Longitud\": \"-90.605153\",\n";
  String ValorMedicion="\"Valor_medicion\": ";
  String Medicion;
  String unidad ="\"Unidad\":";
  String miUni;
  String jsonInicial="";
  String paAroo="";
  String yourdata="{\"Tipo\": 1, \"Carnet\": 201404423, \"Latitud\": \"14.628011\", \"Longitud\": \"-90.605153\", \"Valor_medicion\":"+String(adc_MQ7)+", \"Unidad\": \"ppm\"  }";
  String yourdata1="{\"Tipo\": 2, \"Carnet\": 201404423, \"Latitud\": \"14.628011\", \"Longitud\": \"-90.605153\", \"Valor_medicion\":"+String(adc_MQ135)+", \"Unidad\": \"ppm\"  }";
  String yourdata2="{\"Tipo\": 3, \"Carnet\": 201404423, \"Latitud\": \"14.628011\", \"Longitud\": \"-90.605153\", \"Valor_medicion\":"+String(radiacion)+", \"Unidad\": \"W/m²\"  }";
  
  TipoMedida = Tipo + "3,\n";
     Medicion=ValorMedicion+String(radiacion)+",\n";
     miUni=unidad+"\"w/m^2\"\n";
     jsonInicial= TipoMedida+carnet+latitud+longitud+Medicion+miUni;
     //jsonInicial="{"+TipoMedida+carnet+latitud+longitud+Medicion+miUni+"}";
  // read from port 1, send to port 0:
  if(clienteServidor.connect(server,80)){
    Serial.println("Entro al servidor");
      //Serial.println(jsonInicial);
      Serial.println(yourdata);
      Serial.println(yourdata1);
      Serial.println(yourdata2);
      // Make the HTTP request
  int value = 2.5;  // an arbitrary value for testing
  String content = "{\"JSON_key\": " + String(value) + "}";
  clienteServidor.println("POST /api/medicion HTTP/1.1");
  clienteServidor.println("Host: practica1-ace2.azurewebsites.net");
  clienteServidor.println("Accept: */*");
  clienteServidor.println("Content-Length: " + String(yourdata.length()));
  clienteServidor.println("Content-Type: application/json");
  clienteServidor.println();
  clienteServidor.println(yourdata);
  clienteServidor.println("POST /api/medicion HTTP/1.1");
  clienteServidor.println("Host: practica1-ace2.azurewebsites.net");
  clienteServidor.println("Accept: */*");
  clienteServidor.println("Content-Length: " + String(yourdata1.length()));
  clienteServidor.println("Content-Type: application/json");
  clienteServidor.println();
  clienteServidor.println(yourdata1);
  clienteServidor.println("POST /api/medicion HTTP/1.1");
  clienteServidor.println("Host: practica1-ace2.azurewebsites.net");
  clienteServidor.println("Accept: */*");
  clienteServidor.println("Content-Length: " + String(yourdata2.length()));
  clienteServidor.println("Content-Type: application/json");
  clienteServidor.println();
  clienteServidor.println(yourdata2); 
      //paAroo="^ MQ7: "+String(adc_MQ7)+ " PPM, MQ135: "+String(adc_MQ135)+ " PPM, UV: "+String(radiacion)+ " W/m2";
      //paAroo=String(adc_MQ7)+ ", "+String(adc_MQ135)+ ", "+String(radiacion);
      //blueSerial.print(paAroo);
      //Serial.println(paAroo);
      Serial.println("Done!");
      Serial.println("Disconnecting from server...");
      clienteServidor.stop();
  }else{
    Serial.println("Fallo la conexion");
    Serial.println("Desconectando.");
    clienteServidor.stop();
  }
  Serial.println("Salio");
  //paAroo="^ MQ7: "+String(adc_MQ7)+ " PPM, MQ135: "+String(adc_MQ135)+ " PPM, UV: "+String(radiacion)+ " W/m2";
  paAroo=String(adc_MQ7)+ ", "+String(adc_MQ135)+ ", "+String(radiacion);
  blueSerial.print(paAroo);
  Serial.println(paAroo);
  delay(2000);
  //Serial.println(adc_MQ7);
  //Serial.println(adc_MQ135);
  //Serial.println(radiacion);
  /*WiFiEspClient cliente = servidor.available();
  if(cliente){
    
  }*/
  if (Serial1.available()) {
    int inByte = Serial1.read();
    Serial.write(inByte);
  }

  // read from port 0, send to port 1:
  if (Serial.available()) {
    int inByte = Serial.read();
    Serial1.write(inByte);
  }
  delay(1000);
}
void ejecutarWIFI(){
  
}
