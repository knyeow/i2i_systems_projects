package com.hazelcastdemoapp;

import com.hazelcast.core.Hazelcast;
import com.hazelcast.core.HazelcastInstance;
import com.hazelcast.client.HazelcastClient;
import com.hazelcast.client.config.ClientConfig;

import java.util.Map;

public class App 
{
    public static void main( String[] args )
    {
        ClientConfig clientConfig = new ClientConfig();

        clientConfig.setClusterName("dev");

        clientConfig.getNetworkConfig().addAddress("localhost:5701");

        HazelcastInstance client = HazelcastClient.newHazelcastClient(clientConfig);
        Map<Integer, Person> map = client.getMap("map");


        int dataAmount = 10000;
        int appliedAmount = 0;
        for(int i=0;i < dataAmount;i++)
        {
            Person person =  new Person("harun" + i);
            map.put(i,person);
            appliedAmount++;
        }

        System.out.println(appliedAmount + " data added to map");


        for(int i =0; i< appliedAmount; i++)
        {
            Person person = map.get(i);
            System.out.println(person);
        }

        Hazelcast.shutdownAll();
    }
}
