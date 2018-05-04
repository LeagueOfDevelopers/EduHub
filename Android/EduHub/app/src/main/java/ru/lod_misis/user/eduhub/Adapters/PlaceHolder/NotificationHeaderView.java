package ru.lod_misis.user.eduhub.Adapters.PlaceHolder;

import android.util.Log;
import android.widget.TextView;

import com.example.user.eduhub.R;
import com.mindorks.placeholderview.annotations.Layout;
import com.mindorks.placeholderview.annotations.Resolve;
import com.mindorks.placeholderview.annotations.View;
import com.mindorks.placeholderview.annotations.expand.Parent;
import com.mindorks.placeholderview.annotations.expand.SingleTop;

import org.joda.time.DateTime;

import java.util.Date;

import ru.lod_misis.user.eduhub.Models.Notivications.Notification;

/**
 * Created by User on 06.04.2018.
 */
@Parent
@SingleTop
@Layout(R.layout.notification_header)
public class NotificationHeaderView {
    @View(R.id.notification_text)
    private TextView notificationText;

    @View(R.id.notification_date)
    private TextView date;
    Notification notification;

    public NotificationHeaderView(Notification notification) {
        this.notification = notification;
    }

    @Resolve
    private void onResolved() {
        notificationText.setText(notification.getText());
        DateTime dt = new DateTime( notification.getDate() ) ;


        Long dateInt=dt.toDate().getTime()/1000/60/60;

        Log.d("Date",dateInt.toString());
        Long days;
        Long mes;
        if(new Date().getTime()/1000/60/60-dateInt<1){
            date.setText("<часа назад");
        }else{
            if(new Date().getTime()/1000/60/60-dateInt<24){
                date.setText(new Date().getTime()/1000/60/60-dateInt+"ч. назад");
            }
            if(new Date().getTime()/1000/60/60-dateInt>24){
                days=(new Date().getTime()/1000/60/60-dateInt)/24;
                if(days==1){
                    date.setText(days+" день назад");
                }else{
                    if(days<5) {
                        date.setText(days + " дня назад");
                    }else{
                        if(days>31){
                            mes=days/31;
                            if(mes==1){
                                date.setText("месяяц назад");
                            }else {
                                if(mes<5){
                                    date.setText(mes+" месяца назад");
                                }else{
                                    if(mes<12){
                                        date.setText(mes+" месяцев назад");
                                    }else{
                                        date.setText("больше года назад");
                                    }
                                }

                            }
                        }else{
                            date.setText(days + " дней назад");
                        }
                    }



                }


            }
        }
    }
}
