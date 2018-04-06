package ru.lod_misis.user.eduhub.Adapters.PlaceHolder;

import android.widget.TextView;

import com.example.user.eduhub.R;
import com.mindorks.placeholderview.annotations.Layout;
import com.mindorks.placeholderview.annotations.Resolve;
import com.mindorks.placeholderview.annotations.View;
import com.mindorks.placeholderview.annotations.expand.Parent;
import com.mindorks.placeholderview.annotations.expand.SingleTop;

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
    private TextView notificationDate;
    Notification notification;

    public NotificationHeaderView(Notification notification) {
        this.notification = notification;
    }

    @Resolve
    private void onResolved() {
        notificationText.setText(notification.getText());
        notificationDate.setText(notification.getDate().toString());
    }
}
