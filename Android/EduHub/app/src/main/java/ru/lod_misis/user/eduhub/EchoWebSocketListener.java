package ru.lod_misis.user.eduhub;

import android.provider.SyncStateContract;
import android.util.Log;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.HttpURLConnection;
import java.net.InetAddress;
import java.net.Proxy;
import java.net.Socket;
import java.net.URL;
import java.net.UnknownHostException;

import okhttp3.Request;
import okhttp3.Response;
import okhttp3.WebSocket;
import okhttp3.WebSocketListener;
import okio.ByteString;
import ru.lod_misis.user.eduhub.Interfaces.IChatSocket;
import ru.lod_misis.user.eduhub.Interfaces.View.IChatView;
import ru.lod_misis.user.eduhub.Models.Group.Message;


    public final class EchoWebSocketListener extends WebSocketListener {
        private static final int NORMAL_CLOSURE_STATUS = 1000;
        private String token;

        public EchoWebSocketListener(String token) {
            this.token = token;
        }

        @Override
        public void onOpen(WebSocket webSocket, Response response) {
            webSocket.send("token=Bearer "+token+"/");
            Log.d("openListener","Началося");
        }
        @Override
        public void onMessage(WebSocket webSocket, String text) {
            Log.d("message",text);
        }
        @Override
        public void onMessage(WebSocket webSocket, ByteString bytes) {
            Log.d("bytes",bytes.toString());
        }
        @Override
        public void onClosing(WebSocket webSocket, int code, String reason) {
            webSocket.close(NORMAL_CLOSURE_STATUS, null);

        }
        @Override
        public void onFailure(WebSocket webSocket, Throwable t, Response response) {
            Log.e("Error socket",t.toString());
        }
    }

