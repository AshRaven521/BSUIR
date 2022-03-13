package com.company;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.Socket;

public class Attack1 {
    private final int PORT = 8080;
    Attack1() {
        try {
            Socket clientSocket = new Socket("localhost", PORT);
            new BufferedReader(new InputStreamReader(System.in));
            BufferedReader in = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
            BufferedWriter out = new BufferedWriter(new OutputStreamWriter(clientSocket.getOutputStream()));
            String request = (new TCP())
                    .with_source_port(9000)
                    .with_destination_port(PORT)
                    .with_syn(1)
                    .get_String();
            out.write(request + "\n");
            out.flush();
            String message = in.readLine();
            System.out.println(message);
            TCP responce = TCP.from_string(message);
            System.out.println(responce.syn + " " + responce.ack);
        } catch (Exception ex) {
            ex.printStackTrace();
        }

    }
}

