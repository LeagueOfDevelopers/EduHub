package ru.lod_misis.user.eduhub.Adapters.PlaceHolder;

import android.content.Context;
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
import ru.lod_misis.user.eduhub.Models.User;

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
    RelativeLayout.LayoutParams layoutParams;
    


    public MessageView(Message message, User user, Context context) {
        this.user=user;
        this.message = message;
        this.context=context;
         dp =context.getResources().getDisplayMetrics().density;
         layoutParams=new RelativeLayout.LayoutParams(300*(int)dp, ViewGroup.LayoutParams.WRAP_CONTENT);

    }

    @Resolve
    private void onResolved() {
        if(message.getSenderId().equals(user.getUserId())){
            layoutParams.addRule(RelativeLayout.ALIGN_PARENT_RIGHT);
            layoutParams.setMargins(7*(int)dp,8*(int)dp,7*(int)dp,8*(int)dp);
            cardOfMessage.setLayoutParams(layoutParams);
        }
        messageText.setText(message.getTextMessage());
        DateTime dt = new DateTime(message.getTime());
        name.setText(message.getSenderName());
        role.setText(message.getSenderRole());

        Long dateInt = dt.toDate().getTime() / 1000 / 60 / 60;
        Log.d("Date", dateInt.toString());
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
