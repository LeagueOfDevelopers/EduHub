package ru.lod_misis.user.eduhub.Models.Notivications;

import java.util.Date;

/**
 * Created by User on 06.04.2018.
 */

public class Notification {
    private String text;
    private String date;

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }

    public String getDate() {
        return date;
    }

    public void setDate(String date) {
        this.date = date;
    }
}
