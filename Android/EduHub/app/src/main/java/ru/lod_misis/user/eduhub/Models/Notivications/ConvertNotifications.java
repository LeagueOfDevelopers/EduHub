package ru.lod_misis.user.eduhub.Models.Notivications;

import android.util.Log;

import com.google.gson.Gson;

import retrofit2.converter.gson.GsonConverterFactory;

public class ConvertNotifications {
    public Notification convertCommonNotificationToNotofication(Notifications notifications){
        Notification notification=new Notification();
        StringBuilder jsonBuilder=new StringBuilder();

        for (char char2:notifications.getEventInfo().toCharArray()
                ) {
            if(char2!='/'){
                jsonBuilder.append(char2);
            }
        }
        String json=jsonBuilder.toString();
        notification.setDate(notifications.getOccurredOn());
        switch (notifications.getEventType()){

            case 1:{
                Gson gson=new Gson();

                CourseFinished courseFinished=gson.fromJson(json,CourseFinished.class);
                notification.setText("В группе "+courseFinished.getGroupTitle()+" был закончен обучающий курс");

                return notification;
            }
            case 2:{
                Gson gson=new Gson();

                CourseAccepted courseAccepted=gson.fromJson(json,CourseAccepted.class);
                notification.setText("В группе "+courseAccepted.getGroupTitle()+" был принят учебный план");


                return notification;}
            case 3:{
                Gson gson=new Gson();

                CourseDeclined courseDeclined=gson.fromJson(json,CourseDeclined.class);
                notification.setText("В группе "+courseDeclined.getGroupTitle()+" был отклонен учебный план");

                return notification;}
            case 4:{
                Gson gson=new Gson();
                CourseSuggested courseSuggested=gson.fromJson(json,CourseSuggested.class);
                notification.setText("В группе "+courseSuggested.getGroupTitle()+" был предложен учебный план");

                return notification;}
            case 5:{
                Gson gson=new Gson();
                GroupFormed groupFormed=gson.fromJson(json,GroupFormed.class);
                notification.setText("Группа "+groupFormed.getGroupTitle()+" укомплектована");

                return notification;
            }
            case 6:{
                Gson gson=new Gson();
                InvitationAccepted invitationAccepted=gson.fromJson(json,InvitationAccepted.class);
                notification.setText(invitationAccepted.getInvitedName()+" принял ваше приглашение");

                return notification;}
            case 7:{
                Gson gson=new Gson();
                InvitationDeclined invitationDeclined=gson.fromJson(json,InvitationDeclined.class);
                notification.setText(invitationDeclined.getInvitedName()+" отклонил ваше приглашение");

                return notification;}
            case 8:{
                notification=null;
                return notification;}
            case 9:{
                Gson gson=new Gson();
                MemberLeft memberLeft=gson.fromJson(json,MemberLeft.class);
                notification.setText(memberLeft.getUsername()+" вышел из "+memberLeft.getGroupTitle());

                return notification;}
            case 10:{
                Gson gson=new Gson();
                NewCreator newCreator=gson.fromJson(json,NewCreator.class);
                notification.setText(newCreator.getNewCreatorUsername()+" заменил "+newCreator.getExCreatorUsername()+"на роле Создателя");

                return notification;}
            case 11:{
                Gson gson=new Gson();
                GsonConverterFactory.create(gson);
                NewMember newMember=gson.fromJson(notifications.getEventInfo(),NewMember.class);
                Log.d("JSON",json);
                Log.d("name",newMember.getUsername()+"");
                notification.setText("Встречаем нового участника группы "+newMember.getGroupTitle()+"-"+newMember.getUsername()+"!!!");

                return notification;}
            case 12:{
                Gson gson=new Gson();
                ReportMessage reportMessage=gson.fromJson(json,ReportMessage.class);
                notification.setText("Новая жалоба от "+reportMessage.getSenderName());

                return notification;}
            case 13:{
                Gson gson=new Gson();
                NewReview newReview=gson.fromJson(json,NewReview.class);
                notification.setText(newReview.getReviewerName()+" оставил о Вас отзыв");

                return notification;}
            case 14:{
                Gson gson=new Gson();
                SunctionApplied sunctionApplied=gson.fromJson(json,SunctionApplied.class);
                notification.setText("к вам применены санкции");

                return notification;}
            case 16:{
                Gson gson=new Gson();
                TeacherFound teacherFound=gson.fromJson(json,TeacherFound.class);
                notification.setText("В группу "+teacherFound.getGroupTitle()+" присоединился учитель");

                return notification;}
            case 15:{
                Gson gson=new Gson();
                TeacherFound teacherFound=gson.fromJson(json,TeacherFound.class);
                notification.setText("В группу \""+teacherFound.getGroupTitle()+"\" присоединился учитель");

                return notification;}


        }
        return notification;
    }
}
