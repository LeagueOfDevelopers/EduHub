package ru.lod_misis.user.eduhub.Adapters.PlaceHolder;

import android.content.Context;
import android.graphics.Color;
import android.support.v7.widget.CardView;
import android.util.Log;
import android.view.ViewGroup;
import android.widget.RelativeLayout;
import android.widget.TextView;

import com.example.user.eduhub.R;
import com.mindorks.placeholderview.annotations.Layout;
import com.mindorks.placeholderview.annotations.Resolve;
import com.mindorks.placeholderview.annotations.View;
import com.mindorks.placeholderview.annotations.expand.Parent;
import com.mindorks.placeholderview.annotations.expand.SingleTop;

import org.joda.time.DateTime;

import java.util.Date;

import ru.lod_misis.user.eduhub.Models.Group.Message;
import ru.lod_misis.user.eduhub.Models.Notivications.ConvertNotifications;
import ru.lod_misis.user.eduhub.Models.Notivications.Notification;
import ru.lod_misis.user.eduhub.Models.Notivications.Notifications;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Presenters.NotificationsPresenter;

/**
 * Created by User on 20.04.2018.
 */
@Parent
@SingleTop
@Layout(R.layout.message)
public class MessageView {
    @View(R.id.card_of_message)
    private CardView cardOfMessage;
    @View(R.id.role)
    private TextView role;
    @View(R.id.name)
    private TextView name;
    @View(R.id.time)
    private TextView date;
    @View(R.id.message)
    private TextView messageText;



    private Message message;
    private User user;
    Context context;
    float dp;
    static int i=0;
    RelativeLayout.LayoutParams layoutParams;
    ConvertNotifications convertNotifications=new ConvertNotifications();

    public MessageView(Message message, User user, Context context) {
        this.user=user;
        this.message = message;
        this.context=context;
         dp =context.getResources().getDisplayMetrics().density;
         layoutParams=new RelativeLayout.LayoutParams(300*(int)dp, ViewGroup.LayoutParams.WRAP_CONTENT);


    }

    @Resolve
    private void onResolved() {
        if(message.getNotificationInfo()==null){
        if(message.getSenderId().equals(user.getUserId())){
            layoutParams.addRule(RelativeLayout.ALIGN_PARENT_RIGHT);
            layoutParams.setMargins(7*(int)dp,8*(int)dp,7*(int)dp,8*(int)dp);
            cardOfMessage.setLayoutParams(layoutParams);

            i++;
        }else{
            layoutParams.addRule(RelativeLayout.ALIGN_PARENT_LEFT);
            layoutParams.setMargins(7*(int)dp,8*(int)dp,7*(int)dp,8*(int)dp);
            cardOfMessage.setLayoutParams(layoutParams);
        }
        messageText.setText(message.getText());

        name.setText(message.getSenderName());
       // role.setText(message.getSenderRole());
            operationWithDate(message.getSentOn());

        }else{
            Notifications notifications=new Notifications();
            notifications.setEventInfo(message.getNotificationInfo());
            notifications.setEventType(message.getNotificationType());
            notifications.setId(message.getId()+"");
            notifications.setOccurredOn(message.getSentOn());
            Notification notification=convertNotifications.convertCommonNotificationToNotofication(notifications);
            messageText.setText(notification.getText());
            role.setVisibility(android.view.View.GONE);
            date.setVisibility(android.view.View.GONE);
            RelativeLayout.LayoutParams layout= new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WRAP_CONTENT, ViewGroup.LayoutParams.WRAP_CONTENT);
            layout.setMargins(2*(int)dp,7*(int)dp,2*(int)dp,7*(int)dp);
            layout.addRule(RelativeLayout.CENTER_IN_PARENT);
            messageText.setLayoutParams(layout);
            messageText.setTextSize(5*dp);
            layoutParams=new RelativeLayout.LayoutParams(300*(int)dp, ViewGroup.LayoutParams.WRAP_CONTENT);
            layoutParams.addRule(RelativeLayout.CENTER_HORIZONTAL);
            layoutParams.setMargins(2*(int)dp,5*(int)dp,2*(int)dp,5*(int)dp);
            cardOfMessage.setBackgroundColor(Color.GRAY);
            cardOfMessage.setLayoutParams(layoutParams);



        }
    }
    private void operationWithDate(String data){
        DateTime dt = new DateTime(data);
        Long dateInt = dt.toDate().getTime() / 1000 / 60 / 60;
        Log.d("messageTime",dateInt.toString()+"||"+new Date().getTime()/1000/60/60);

        Long days;
        Long mes;
        if (new Date().getTime() / 1000 / 60 / 60 - dateInt == 0) {
            date.setText("<часа назад");
        } else {
            if (new Date().getTime() / 1000 / 60 / 60 - dateInt < 24) {
                date.setText(new Date().getTime() / 1000 / 60 / 60 - dateInt + "ч. назад");
            }
            if (new Date().getTime() / 1000 / 60 / 60 - dateInt > 24) {
                days = (new Date().getTime() / 1000 / 60 / 60 - dateInt) / 24;
                if (days == 1) {
                    date.setText(days + " день назад");
                } else {
                    if (days < 5) {
                        date.setText(days + " дня назад");
                    } else {
                        if (days > 31) {
                            mes = days / 31;
                            if (mes == 1) {
                                date.setText("месяяц назад");
                            } else {
                                if (mes < 5) {
                                    date.setText(mes + " месяца назад");
                                } else {
                                    if (mes < 12) {
                                        date.setText(mes + " месяцев назад");
                                    } else {
                                        date.setText("больше года назад");
                                    }
                                }

                            }
                        } else {
                            date.setText(days + " дней назад");
                        }
                    }


                }


            }
        }
    }
}
