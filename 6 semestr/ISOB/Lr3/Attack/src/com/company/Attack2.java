package com.company;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.Socket;

public class Attack2 {
    private final int PORT = 8080;
    Attack2() {
        try {
            Socket clientSocket = new Socket("localhost", PORT);
            new BufferedReader(new InputStreamReader(System.in));
            new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
            BufferedWriter out = new BufferedWriter(new OutputStreamWriter(clientSocket.getOutputStream()));

            for(int i = 0; i < 10; ++i) {
                String request = (new TCP())
                        .with_source_port(1488 + i)
                        .with_destination_port(PORT)
                        .with_syn(1)
                        .get_String();
                out.write(request + "\n");
                out.flush();
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }
}