package ru.lod_misis.user.eduhub.Models;

import org.joda.time.DateTime;

import java.text.ParseException;
import java.text.SimpleDateFormat;

public class ConverDate {

    public String convertDate(String date,Boolean flag){
        SimpleDateFormat simpleDateFormat=new SimpleDateFormat("dd.MM.yyyy");
        if(flag){
            return simpleDateFormat.format(new DateTime(Long.valueOf(date)).toDate());
        }else{
        return simpleDateFormat.format(DateTime.parse(date).toDate());}
    }
    public String convertDateForRequest(Long date){
        SimpleDateFormat simpleDateFormat=new SimpleDateFormat("MM.dd.yyyy");
        return simpleDateFormat.format(new DateTime(Long.valueOf(date)).toDate());
    }
}
