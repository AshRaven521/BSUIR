package com.company;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.SocketException;
import java.util.ArrayList;
import java.util.Iterator;

class Server {
    private final int PORT = 8080;
    private final ArrayList<Integer> SYNReceivedList = new ArrayList();
    private final ArrayList<Integer> CONNECTED = new ArrayList();
    private final int SYN_RECEIVED_MAXSIZE = 5;
    private final int CONNECTED_MAXSIZE = 5;
    private static Socket clientSocket;
    private static ServerSocket server;
    private static BufferedReader in;
    private static BufferedWriter out;

    Server() throws IOException {
        try {
            server = new ServerSocket(this.PORT);
            System.out.println("SERVER IS WORKING...");
            System.out.println("------------------------------------------------");
            System.out.println("WAIT PLEASE...");
            System.out.println("------------------------------------------------");
            clientSocket = server.accept();
            System.out.println("SOME CLIENT IS CONNECTED");

            try {
                in = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
                out = new BufferedWriter(new OutputStreamWriter(clientSocket.getOutputStream()));

                while(true) {
                    String word = in.readLine();
                    System.out.println("MESSAGE: " + word.replaceAll(" ", ""));
                    System.out.println("------------------------------------------------");
                    TCP request = TCP.from_string(word);
                    if (request.destination_port == this.PORT && !this.CONNECTED.contains(request.source_port)) {
                        if (this.SYNReceivedList.contains(request.source_port)) {
                            this.addToConnected(request);
                        } else {
                            this.addToSynReceived(request, clientSocket);
                        }
                    }
                }
            } catch (SocketException se) {
                System.out.println("------------------------------------------------");
                System.out.println("CLIENT DOESN'T RESPONSE!");
            } catch (Exception ex) {
                System.out.println("------------------------------------------------");
                System.out.println("ERROR! Something wrong...");
                ex.printStackTrace();
            } finally {
                System.out.println("------------------------------------------------");
                System.out.println("SOCKET CLOSING...");
                clientSocket.close();
                in.close();
                out.close();
            }
        } catch (IOException ioex) {
            System.err.println(ioex);
        } finally {
            System.out.println("SERVER IS DOWN");
            server.close();
        }
    }

    private void addToSynReceived(TCP request, Socket clientSocket) throws Exception {
        TCP response = (new TCP())
                .with_source_port(this.PORT)
                .with_destination_port(request.destination_port)
                .with_syn(1)
                .with_ack(1);
        out = new BufferedWriter(new OutputStreamWriter(clientSocket.getOutputStream()));
        out.write(response.get_String() + "\n");
        out.flush();
        this.SYNReceivedList.add(request.source_port);
        System.out.println("NEW SYN HAS BEEN RECEIVED.");
        System.out.print("SYN_RECEIVED: ");

        for (int syn : this.SYNReceivedList){
            System.out.print(syn + " ");
        }

        System.out.print("\nCONNECTED.");
        for (int con : this.CONNECTED){
            System.out.print(con + " ");
        }

        System.out.println("");
        if (this.SYNReceivedList.size() > SYN_RECEIVED_MAXSIZE) {
            System.out.println("TOO MUCH SYN...");
            throw new Exception("SYN IS OVERLOAD!");
        }
    }

    void addToConnected(TCP request) throws Exception {
        this.SYNReceivedList.remove(this.SYNReceivedList.indexOf(request.source_port));
        this.CONNECTED.add(request.source_port);
        System.out.println("NEW CLIENT HAS BEEN CONNECTED.");
        System.out.print("SYN_RECEIVED:");

        for (int syn : this.SYNReceivedList){
            System.out.print(syn + " ");
        }

        System.out.print("\nCONNECTED:");
        for (int con : this.CONNECTED){
            System.out.print(con + " ");
        }

        System.out.println("");
        if (this.CONNECTED.size() > this.CONNECTED_MAXSIZE) {
            System.out.println("TOO MUCH CONNECTIONS...");
            throw new Exception("SERVER IS OVERLOAD!");
        }
    }
}
