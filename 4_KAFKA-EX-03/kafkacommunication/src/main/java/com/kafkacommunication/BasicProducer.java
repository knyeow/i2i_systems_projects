package com.kafkacommunication;

import java.util.Properties;

import org.apache.kafka.clients.producer.KafkaProducer;
import org.apache.kafka.clients.producer.ProducerRecord;

public class BasicProducer {

  public static final String TOPIC = "hello-world-topic";

  public static void main(String[] args) {

    System.out.println("*** Starting Basic Producer ***");

    Properties settings = new Properties();

    settings.put("client.id", "basic-producer");
    settings.put("bootstrap.servers", "localhost:9092");
    settings.put("key.serializer", "org.apache.kafka.common.serialization.StringSerializer");
    settings.put("value.serializer", "org.apache.kafka.common.serialization.StringSerializer");

    Student students[] = new Student[]
    {new Student(1,"harun"),new Student(2,"ege")};

    try (KafkaProducer<String, String> producer = new KafkaProducer<>(settings)) {

        for(int i= 0; i < students.length;i++)
        {
            final String key = "value-" + i;
            final String value = students[i].toString();

            System.out.println("#### Sending " + value + " ###");

            producer.send(new ProducerRecord<>(TOPIC, key, value));
        }

    }
  }
}