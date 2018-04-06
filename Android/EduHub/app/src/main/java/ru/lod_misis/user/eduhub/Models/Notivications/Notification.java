package ru.lod_misis.user.eduhub.Models.Notivications;

import java.util.Date;

/**
 * Created by User on 06.04.2018.
 */

public class Notification {
    private String text;
    private Date date;

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }

    public Date getDate() {
        return date;
    }

    public void setDate(Date date) {
        this.date = date;
    }
}
