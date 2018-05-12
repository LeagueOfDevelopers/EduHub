package ru.lod_misis.user.eduhub.Fragments;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v7.widget.CardView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.ImageButton;

import okhttp3.OkHttpClient;
import okhttp3.Request;
import ru.lod_misis.user.eduhub.Adapters.PlaceHolder.MessageView;
import ru.lod_misis.user.eduhub.EchoWebSocketListener;
import ru.lod_misis.user.eduhub.Fakes.FakeChatPresenter;
import ru.lod_misis.user.eduhub.Fakes.FakesButton;
import ru.lod_misis.user.eduhub.Interfaces.Presenters.IChatPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.IChatView;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Models.Group.Message;
import ru.lod_misis.user.eduhub.Models.Group.NewMessage;
import ru.lod_misis.user.eduhub.Models.Group.NewMessageResponse;
import ru.lod_misis.user.eduhub.Models.Notivications.CourseSuggested;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Presenters.ChatPresenter;

import com.example.user.eduhub.R;
import com.google.gson.Gson;
import com.mindorks.placeholderview.ExpandablePlaceHolderView;
import com.neovisionaries.ws.client.WebSocket;
import com.neovisionaries.ws.client.WebSocketAdapter;
import com.neovisionaries.ws.client.WebSocketException;
import com.neovisionaries.ws.client.WebSocketFactory;
import com.neovisionaries.ws.client.WebSocketFrame;

import org.joda.time.DateTime;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Map;
import java.util.concurrent.TimeUnit;

/**
 * Created by User on 23.12.2017.
 */

public class ChatFragment extends Fragment implements IChatView  {
    private User user;
    private Group group;
    private Boolean flag=false;
    Context context;
    private OkHttpClient client;
    WebSocket ws;

    //private FakeMessageRep messageRep=new FakeMessageRep();
    ArrayList<Message> messages=new ArrayList<>();
    ExpandablePlaceHolderView expandablePlaceHolderView;
    FakesButton fakesButton=new FakesButton();
    FakeChatPresenter fakeChatPresenter=new FakeChatPresenter(this);
    ChatPresenter chatPresenter;
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.chat, null);
        context=getContext();
        chatPresenter=new ChatPresenter(this,context);
        Log.d("context",context.toString());

        final EditText editTextMessage=v.findViewById(R.id.edit_message);
        if(flag){
            CardView message_text=v.findViewById(R.id.message_text);
            message_text.setVisibility(View.GONE);
        }
        expandablePlaceHolderView=v.findViewById(R.id.messages_list_layout);
        ImageButton btn=v.findViewById(R.id.enter_message_button);
        if(!fakesButton.getCheckButton()){
            if(user!=null&&group!=null){
                chatPresenter.loadAllMessages(user.getToken(),group.getGroupInfo().getId());}
        }else{
            if(user!=null&&group!=null){
                fakeChatPresenter.loadAllMessages(user.getToken(),group.getGroupInfo().getId());}
        }




        btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(!editTextMessage.getText().toString().equals("")){
                    if(fakesButton.getCheckButton()){
                    Message newMessage=new Message(user.getName(),user.getUserId(),user.getRole(),new Date()+"",editTextMessage.getText().toString(),1);
                    expandablePlaceHolderView.addView(new MessageView(newMessage,user,context));}
                    else{
                        chatPresenter.sendMessage(user.getToken(),group.getGroupInfo().getId(),editTextMessage.getText().toString());
                    }
                    editTextMessage.setText("");

                }
            }
        });
        return v;
    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if(!flag){
        client=new OkHttpClient.Builder()
                .build();
        startListenerWebSocket();}
    }

    public void setUser(User user) {
        this.user = user;
    }

    public void setFlag(Boolean flag) {
        this.flag = flag;
    }

    public void setGroup(Group group) {
        this.group = group;
    }



    @Override
    public void showLoading() {

    }

    @Override
    public void stopLoading() {

    }

    @Override
    public void getError(Throwable error) {

    }


    @Override
    public void getMessages(ArrayList<Message> messages) {
        for (Message message:messages) {

            expandablePlaceHolderView.addView(new MessageView(message,user,context));
        }
    }

    @Override
    public void getEmptyMessages() {

    }

    @Override
    public void newMessage(NewMessageResponse message) {
        Log.d("123",123+"");
        Message message1=new Message("",message.getGroupId()+"","","",message.getText(),0);
        expandablePlaceHolderView.addView(new MessageView(message1,user,context));
    }
    private void startListenerWebSocket(){

        try {
            ws = new WebSocketFactory().createSocket("ws://85.143.104.47:2411/api/sockets/creation?token="+user.getToken());
            ws.addListener(new WebSocketAdapter(){
                @Override
                public void onError(WebSocket websocket, WebSocketException cause) throws Exception {
                    super.onError(websocket, cause);
                    Log.e("Error Socket",cause.toString());
                }

                @Override
                public void onConnected(WebSocket websocket, Map<String, List<String>> headers) throws Exception {
                    super.onConnected(websocket, headers);
                    Log.e("openListener","Началося");
                }

                @Override
                public void onTextMessage(WebSocket websocket, String message) throws Exception {
                    // Received a text message.
                    Log.d("message",message);
                    Gson gson=new Gson();
                    NewMessageResponse message1=gson.fromJson(message,NewMessageResponse.class);
                    newMessage(message1);
                }

                @Override
                public void onConnectError(WebSocket websocket, WebSocketException exception) throws Exception {
                    super.onConnectError(websocket, exception);
                    Log.e("Error Connect Socket",exception.toString());
                }

                @Override
                public void onDisconnected(WebSocket websocket, WebSocketFrame serverCloseFrame, WebSocketFrame clientCloseFrame, boolean closedByServer) throws Exception {
                    super.onDisconnected(websocket, serverCloseFrame, clientCloseFrame, closedByServer);
                    Log.d("disconected","Disconected");
                }
            });
            ws.connectAsynchronously();
            Log.d("checkOpen",ws.isOpen()+"");
        } catch (IOException e) {
            Log.e("Error Socket",e.toString());
        }

    }


   

    @Override
    public void onDestroy() {
        super.onDestroy();
        if(ws!=null){
        ws.disconnect();}
    }
}
