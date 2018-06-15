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
    @View(R.id.card_of_my_message)
    private CardView MyCardOfMessage;
    @View(R.id.my_role)
    private TextView MyRole;
    @View(R.id.my_name)
    private TextView MyName;
    @View(R.id.my_time)
    private TextView MyDate;
    @View(R.id.my_message)
    private TextView MyMessageText;
    @View(R.id.notification)
    private TextView notification;



    private Message message;
    private User user;
    Context context;
    float dp;
    static int i=0;
    RelativeLayout.LayoutParams layoutParams;
    RelativeLayout.LayoutParams layoutParams2;
    ConvertNotifications convertNotifications=new ConvertNotifications();

    public MessageView(Message message, User user, Context context) {
        this.user=user;
        this.message = message;
        this.context=context;
         dp =context.getResources().getDisplayMetrics().density;



    }

    @Resolve
    private void onResolved() {
        Log.d("Id",user.getUserId());
        if(message.getMessageType()==0){

        if(message.getSenderId().toString().equals(user.getUserId())){
            MyCardOfMessage.setVisibility(android.view.View.VISIBLE);
            cardOfMessage.setVisibility(android.view.View.GONE);
            notification.setVisibility(android.view.View.GONE);
            MyMessageText.setText(message.getText());

            MyName.setText(message.getSenderName());
            MyRole.setText("");
            operationWithDate(message.getSentOn(),true);

            i++;
        }else {
            MyCardOfMessage.setVisibility(android.view.View.GONE);
            cardOfMessage.setVisibility(android.view.View.VISIBLE);
            notification.setVisibility(android.view.View.GONE);
            messageText.setText(message.getText());

            name.setText(message.getSenderName());
            role.setText("");
            operationWithDate(message.getSentOn(),false);
        }


        }else{
            MyCardOfMessage.setVisibility(android.view.View.GONE);
            cardOfMessage.setVisibility(android.view.View.GONE);
            notification.setVisibility(android.view.View.VISIBLE);
            Notifications notifications=new Notifications();
            notifications.setEventInfo(message.getNotificationInfo());
            notifications.setEventType(message.getNotificationType());
            notifications.setId(message.getId()+"");
            notifications.setOccurredOn(message.getSentOn());
            Notification notification=convertNotifications.convertCommonNotificationToNotofication(notifications);
            this.notification.setText(notification.getText());





        }
    }
    private void operationWithDate(String data,Boolean flag){
        DateTime dt = new DateTime(data);
        Long dateInt = dt.toDate().getTime() / 1000 / 60 / 60;


        Long days;
        Long mes;
        Log.d("Date",dateInt+"");
        Log.d("Now",new Date().getTime()/1000/60/60+"");
        if(flag){
            if (new Date().getTime() / 1000 / 60 / 60 - dateInt < 1) {
                MyDate.setText("<часа назад");
            } else {
                if (new Date().getTime() / 1000 / 60 / 60 - dateInt < 24) {
                    MyDate.setText(new Date().getTime() / 1000 / 60 / 60 - dateInt + "ч. назад");
                }
                if (new Date().getTime() / 1000 / 60 / 60 - dateInt > 24) {
                    days = (new Date().getTime() / 1000 / 60 / 60 - dateInt) / 24;
                    if (days == 1) {
                        MyDate.setText(days + " день назад");
                    } else {
                        if (days < 5) {
                            MyDate.setText(days + " дня назад");
                        } else {
                            if (days > 31) {
                                mes = days / 31;
                                if (mes == 1) {
                                    MyDate.setText("месяяц назад");
                                } else {
                                    if (mes < 5) {
                                        MyDate.setText(mes + " месяца назад");
                                    } else {
                                        if (mes < 12) {
                                            MyDate.setText(mes + " месяцев назад");
                                        } else {
                                            MyDate.setText("больше года назад");
                                        }
                                    }

                                }
                            } else {
                                MyDate.setText(days + " дней назад");
                            }
                        }


                    }


                }
            }
        }else{
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
}
