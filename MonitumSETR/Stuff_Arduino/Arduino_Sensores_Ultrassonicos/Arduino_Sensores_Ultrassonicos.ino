//Programa: Conectando Sensor Ultrassonico HC-SR04 ao Arduino
//Autor: FILIPEFLOP
 
//Carrega a biblioteca do sensor ultrassonico
#include <Ultrasonic.h>
 
//Define os pinos para o trigger e echo
#define pino_trigger_sensor_1 4
#define pino_echo_sensor_1 5

#define pino_trigger_sensor_2 2
#define pino_echo_sensor_2 3
 
//Inicializa o sensor nos pinos definidos acima
Ultrasonic ultrasonic_1(pino_trigger_sensor_1, pino_echo_sensor_1);
Ultrasonic ultrasonic_2(pino_trigger_sensor_2, pino_echo_sensor_2);
 
void setup()
{
  Serial.begin(9600);
  Serial.println("Lendo dados do sensor...");
}
 
void loop()
{
  //Le as informacoes do sensor, em cm e pol
  float cmMsec_1, inMsec_1, cmMsec_2, inMsec_2;
  long microsec_1 = ultrasonic_1.timing();
  long microsec_2 = ultrasonic_2.timing();
  cmMsec_1 = ultrasonic_1.convert(microsec_1, Ultrasonic::CM);
  inMsec_1 = ultrasonic_1.convert(microsec_1, Ultrasonic::IN);
  cmMsec_2 = ultrasonic_2.convert(microsec_2, Ultrasonic::CM);
  inMsec_2 = ultrasonic_2.convert(microsec_2, Ultrasonic::IN);
  //Exibe informacoes no serial monitor
  if (cmMsec_1 < 12){ 
    Serial.print("Distancia em cm sensor 1: ");
    Serial.println(cmMsec_1);
  } if (cmMsec_2 < 12){
    Serial.print("Distancia em cm sensor 2: ");
    Serial.println(cmMsec_2);
  }
  
  //Serial.print(" - Distancia em polegadas sensor 1: ");
  //Serial.println(inMsec_1);
  //Serial.print("Distancia em cm sensor 2: ");
  //Serial.print(cmMsec_2);
  //Serial.print(" - Distancia em polegadas sensor 2: ");
  //Serial.println(inMsec_2);
  delay(1000);
}