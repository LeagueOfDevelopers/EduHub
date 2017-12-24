package com.example.user.eduhub.Fragments;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.ImageButton;

import com.example.user.eduhub.Adapters.MessageAdapter;
import com.example.user.eduhub.Classes.Message;
import com.example.user.eduhub.Classes.User;
import com.example.user.eduhub.Fakes.FakeMessageRep;
import com.example.user.eduhub.R;

import java.util.ArrayList;
import java.util.Date;

/**
 * Created by User on 23.12.2017.
 */

public class Chat extends Fragment {
    private User user;
    private FakeMessageRep messageRep=new FakeMessageRep();
    ArrayList<Message> messages=new ArrayList<>();
    RecyclerView recyclerView;
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.chat, null);
        recyclerView=v.findViewById(R.id.recyclerView);
        final EditText editTextMessage=v.findViewById(R.id.edit_message);
        ImageButton btn=v.findViewById(R.id.enter_message_button);
        recyclerView.setHasFixedSize(true);
        LinearLayoutManager llm = new LinearLayoutManager(getActivity().getApplicationContext());
        recyclerView.setLayoutManager(llm);
        messages=messageRep.LoadMessages();
        final MessageAdapter adapter=new MessageAdapter(messages,user);
        if(messages!=null){
        recyclerView.setAdapter(adapter);}

        btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(!editTextMessage.getText().toString().equals("")){
                    Message newMessage=new Message(user.getName(),user.getRole(),editTextMessage.getText().toString(),new Date());
                   boolean flag= messageRep.SaveNewMessage(newMessage);
                    if(flag){
                        messages.clear();
                        adapter.notifyDataSetChanged();
                        messages.addAll(messageRep.LoadMessages());
                        messages.add(newMessage);
                        adapter.notifyDataSetChanged();


                    }
                }
            }
        });
        return v;
    }

    public void setUser(User user) {
        this.user = user;
    }
}
