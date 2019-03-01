import android.content.Intent;
import android.os.Bundle;
import ketai.net.bluetooth.*;
import ketai.ui.*;
import ketai.net.*;

int rectX, rectY;      // Position of square button
int circleX, circleY;  // Position of circle button
int rectSize = 90;     // Diameter of rect
int circleSize = 93;   // Diameter of circle
color rectColor, circleColor, baseColor;
color rectHighlight, circleHighlight;
color currentColor;
boolean rectOver = false;
boolean circleOver = false;

KetaiBluetooth bt;
KetaiList klist;
int val = 0;
String stringInfo = "";
String Anterior1 = "";
String Anterior2 = "";
String Anterior3 = "";
String[] palabra={"63","300","200"};
int[] angles = { 30, 10, 45 };
void onCreate(Bundle savedInstanceState) {
  super.onCreate(savedInstanceState);
  bt = new KetaiBluetooth(this);
}

void onActivityResult(int requestCode, int resultCode, Intent data) {
  bt.onActivityResult(requestCode, resultCode, data);
}

void setup() {
  fullScreen();
  rectColor = color(0);
  rectHighlight = color(51);
  circleColor = color(255);
  circleHighlight = color(204);
  baseColor = color(102);
  currentColor = baseColor;
  circleX = width/2+400 +circleSize/2+10;
  circleY = height/2+800;
  rectX = width/2+400-rectSize-10;
  rectY = height/2+800-rectSize/2;
  ellipseMode(CENTER);
  fill(255);
  textAlign(LEFT);
  textSize(70);
 
}

void draw() {
  update(mouseX, mouseY);
  background(currentColor);
  
  if (rectOver) {
    fill(rectHighlight);
  } else {
    fill(rectColor);
  }
  stroke(255);
  rect(rectX, rectY, rectSize, rectSize);
  
  if (circleOver) {
    fill(circleHighlight);
  } else {
    fill(circleColor);
  }
  stroke(0);
  ellipse(circleX, circleY, circleSize, circleSize);
  
   if(palabra.length==3){
     fill(203, 0, 0);
     rect(width/2-200, height/2+600,180,-Integer.parseInt(palabra[0])*2);
     fill(200,203,0);
     rect(width/2, height/2+600,180,-Integer.parseInt(palabra[1])*2);
     fill(6,0,203);
     rect(width/2+200, height/2+600,180,-Integer.parseInt(palabra[2])*2);
    //pieChart(800, angles);
    fill(250, 250, 250);
    text("Datos de Sensores. Grupo5. Practica1",20,104);
    fill(203, 0, 0);
    text("MQ7: "+palabra[0]+" PPM",20,204);
    fill(200,203,0);
    text("MQ135: "+palabra[1]+"PPM",20,304);
    fill(6,0,203);
    text("UV: "+palabra[2]+"W/m^2",20,404);
  //text("Datos de Sensores. Grupo5. Practica1"+"\n"+"MQ7: "+palabra[0]+" PPM"+"\n"+"MQ135: "+palabra[1]+"PPM"+"\n"+"UV: "+palabra[2]+"W/m^2",20,104);
  }
 
  delay(200);
}

void update(int x, int y) {
  if ( overCircle(circleX, circleY, circleSize) ) {
    circleOver = true;
    rectOver = false;
  } else if ( overRect(rectX, rectY, rectSize, rectSize) ) {
    rectOver = true;
    circleOver = false;
  } else {
    circleOver = rectOver = false;
  }
}

void mousePressed() {
  if (circleOver) {
  bt.start();
  bt.getPairedDeviceNames();
  bt.connectToDeviceByName("HC-05");
  }
  if (rectOver) {
    bt.stop();
  }
}

boolean overRect(int x, int y, int width, int height)  {
  if (mouseX >= x && mouseX <= x+width && 
      mouseY >= y && mouseY <= y+height) {
    return true;
  } else {
    return false;
  }
}

boolean overCircle(int x, int y, int diameter) {
  float disX = x - mouseX;
  float disY = y - mouseY;
  if (sqrt(sq(disX) + sq(disY)) < diameter/2 ) {
    return true;
  } else {
    return false;
  }
}

void onBluetoothDataEvent(String who, byte[] data) {
  if (data != null) {
  String str = new String(data);
    stringInfo = str;
    //println(stringInfo);
    palabra = str.split(",");
   if(palabra.length==3){
  Anterior1=palabra[0];
  Anterior2=palabra[1];
  Anterior3=palabra[2];
  }
  if(palabra.length<3){
  palabra[0]=Anterior1;
  palabra[1]=Anterior2;
  palabra[2]=Anterior3;
  }else{
  palabra[0]=Anterior1;
  palabra[1]=Anterior2;
  palabra[2]=Anterior3;
  }
    
  }
}
void pieChart(float diameter, int[] data) {
  float lastAngle = 0;
  for (int i = 0; i < data.length; i++) {
    float gray = map(i, 0, data.length, 0, 255);
    fill(gray);
    arc(width/2, height/2, diameter, diameter, lastAngle, lastAngle+radians(data[i]));
    lastAngle += radians(data[i]);
  }
}