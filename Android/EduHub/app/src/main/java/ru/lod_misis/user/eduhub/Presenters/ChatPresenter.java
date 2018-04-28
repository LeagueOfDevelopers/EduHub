package ru.lod_misis.user.eduhub.Presenters;

import android.content.Context;
import android.util.Log;

import java.util.ArrayList;

import io.reactivex.Scheduler;
import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;
import ru.lod_misis.user.eduhub.Interfaces.Presenters.IChatPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.IChatView;
import ru.lod_misis.user.eduhub.Models.Group.Message;
import ru.lod_misis.user.eduhub.Models.Group.NewMessage;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

public class ChatPresenter implements IChatPresenter {
    IChatView chatView;
    EduHubApi eduHubApi;
    ArrayList<Message> messages=new ArrayList<>();

    public ChatPresenter(IChatView chatView,Context context) {
        this.chatView = chatView;
        eduHubApi= RetrofitBuilder.getApi(context);
    }

    @Override
    public void loadAllMessages(String token, String groupId) {
        eduHubApi.loadAllMessages("Bearer "+token,groupId)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(next->{messages=(ArrayList<Message> )next;},
                        throwable -> {
                            Log.e("ChatPresenter",throwable.toString());},
                        ()->{if(messages.size()!=0){
                            chatView.getMessages(messages);
                        }else{
                            chatView.getEmptyMessages();
                        }
                        });

    }

    @Override
    public void sendMessage(String token, String groupId, String messageText) {
        NewMessage newMessage=new NewMessage();
        newMessage.setText(messageText);
        Log.d("MessageSendIt",newMessage.getText());
        eduHubApi.sendMessage("Bearer "+token,groupId,newMessage)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(result->{},
                        throwable -> {Log.e("ChatPresenter2",throwable.toString());});
    }
}
