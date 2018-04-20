package ru.lod_misis.user.eduhub.Fragments;

import android.content.Context;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.widget.CardView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.ImageButton;

import ru.lod_misis.user.eduhub.Adapters.PlaceHolder.MessageView;
import ru.lod_misis.user.eduhub.Fakes.FakeChatPresenter;
import ru.lod_misis.user.eduhub.Fakes.FakesButton;
import ru.lod_misis.user.eduhub.Interfaces.Presenters.IChatPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.IChatView;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Models.Group.Message;
import ru.lod_misis.user.eduhub.Models.User;
import com.example.user.eduhub.R;
import com.mindorks.placeholderview.ExpandablePlaceHolderView;

import java.util.ArrayList;
import java.util.Date;

/**
 * Created by User on 23.12.2017.
 */

public class ChatFragment extends Fragment implements IChatView  {
    private User user;
    private Group group;
    private Boolean flag=false;
    Context context;

    //private FakeMessageRep messageRep=new FakeMessageRep();
    ArrayList<Message> messages=new ArrayList<>();
    ExpandablePlaceHolderView expandablePlaceHolderView;
    FakesButton fakesButton=new FakesButton();
    FakeChatPresenter fakeChatPresenter=new FakeChatPresenter(this);
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.chat, null);
        context=getContext();
        Log.d("context",context.toString());

        final EditText editTextMessage=v.findViewById(R.id.edit_message);
        if(flag){
            CardView message_text=v.findViewById(R.id.message_text);
            message_text.setVisibility(View.GONE);
        }
        expandablePlaceHolderView=v.findViewById(R.id.messages_list_layout);
        ImageButton btn=v.findViewById(R.id.enter_message_button);
        if(!fakesButton.getCheckButton()){

        }else{
            if(user!=null&&group!=null){
                fakeChatPresenter.loadAllMessages(user.getToken(),group.getGroupInfo().getId());}
        }




        btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(!editTextMessage.getText().toString().equals("")){
                    Message newMessage=new Message(user.getName(),user.getUserId(),user.getRole(),new Date(),editTextMessage.getText().toString(),1);
                    expandablePlaceHolderView.addView(new MessageView(newMessage,user,context));

                }
            }
        });
        return v;
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
    public void getAllMessages(ArrayList<Message> messages) {
        for (Message message:messages) {
            expandablePlaceHolderView.addView(new MessageView(message,user,context));
        }

    }

    @Override
    public void getResponse() {

    }
}
