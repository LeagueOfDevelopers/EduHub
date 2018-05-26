package ru.lod_misis.user.eduhub.Fragments;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.Spinner;

import com.example.user.eduhub.R;

import ru.lod_misis.user.eduhub.Adapters.SpinnerAdapter;
import ru.lod_misis.user.eduhub.Adapters.SpinnerAdapterForMemberRole;
import ru.lod_misis.user.eduhub.Adapters.SpinnerAdapterForSex;
import ru.lod_misis.user.eduhub.Interfaces.Presenters.INotificationsSettingsPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.INotificationsSettingsView;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Models.UserProfile.ChangeSettingNotification;
import ru.lod_misis.user.eduhub.Models.UserProfile.NotificationsSettings;
import ru.lod_misis.user.eduhub.Presenters.NotificationsSettingsPresenter;

public class NotificationSettings extends Fragment implements INotificationsSettingsView {
    LinearLayout mainLayout;

    Spinner courseFinished;
    Spinner courseAccepted;
    Spinner courseRejected;
    Spinner courseOffered;
    Spinner fullGroupSpinner;
    Spinner participantLeft;
    Spinner changeCreator;
    Spinner newParticipant;
    Spinner teacherFind;

    Spinner getReview;
    Spinner yourInviteAccepted;
    Spinner yourInviteRejected;
    Spinner getNewInvite;

    RelativeLayout reportMessage;
    RelativeLayout sanctionsToAdminCard;
    RelativeLayout sanctionsToUserCard;
    Spinner reportMessageSpinner;
    Spinner sanctionsToAdmin;
    Spinner sanctionsToUser;
    Spinner notSanctions;
    INotificationsSettingsPresenter notificationsSettingsPresenter;
    User user;

    public void setUser(User user) {
        this.user = user;
    }

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.notification_settings_fragment, null);
        Toolbar toolbar = getActivity().findViewById(R.id.toolbar);
        toolbar.setTitle("Настройка уведомлений");
        notificationsSettingsPresenter = new NotificationsSettingsPresenter(this, getContext());
        courseFinished = v.findViewById(R.id.end_course);
        courseAccepted = v.findViewById(R.id.course_accepted);
        courseRejected = v.findViewById(R.id.course_rejected);
        courseOffered = v.findViewById(R.id.course_offered);
        fullGroupSpinner = v.findViewById(R.id.full_group);
        participantLeft = v.findViewById(R.id.left_paticipant);
        changeCreator = v.findViewById(R.id.сchange_creator_role);
        newParticipant = v.findViewById(R.id.new_participant);
        teacherFind = v.findViewById(R.id.find_teacher);

        mainLayout=v.findViewById(R.id.main_layout);
        mainLayout.setVisibility(View.GONE);

        reportMessage = v.findViewById(R.id.report_message_card);
        sanctionsToAdminCard = v.findViewById(R.id.sanctions_to_admin_card);
        sanctionsToUserCard = v.findViewById(R.id.sanctions_to_user_card);

        getReview = v.findViewById(R.id.get_review);
        yourInviteAccepted = v.findViewById(R.id.your_invite_accepted);
        yourInviteRejected = v.findViewById(R.id.your_invite_rejected);
        getNewInvite = v.findViewById(R.id.get_new_invite);

        reportMessageSpinner = v.findViewById(R.id.report_message);
        sanctionsToAdmin = v.findViewById(R.id.sanctions_to_admin);
        sanctionsToUser = v.findViewById(R.id.sanctions_to_user);
        notSanctions = v.findViewById(R.id.not_sunctions);

        String[] roles = {"", "Никуда", "На почту", "На сайт", "Везде",};
        SpinnerAdapter adapter = new SpinnerAdapter(getContext(), R.layout.spinner_item2, roles);

        courseFinished.setAdapter(adapter);

        courseAccepted.setAdapter(adapter);

        courseRejected.setAdapter(adapter);

        courseOffered.setAdapter(adapter);

        fullGroupSpinner.setAdapter(adapter);

        participantLeft.setAdapter(adapter);

        changeCreator.setAdapter(adapter);

        newParticipant.setAdapter(adapter);

        teacherFind.setAdapter(adapter);

        getReview.setAdapter(adapter);

        yourInviteAccepted.setAdapter(adapter);

        yourInviteRejected.setAdapter(adapter);

        getNewInvite.setAdapter(adapter);


        reportMessageSpinner.setAdapter(adapter);

        sanctionsToAdmin.setAdapter(adapter);

        sanctionsToUser.setAdapter(adapter);

        notSanctions.setAdapter(adapter);



        notificationsSettingsPresenter.getSettings(user.getToken());


        return v;

    }

    @Override
    public void getSettings(NotificationsSettings settings) {
        courseFinished.setSelection(Integer.valueOf(settings.getCourseFinished()),false);
        courseAccepted.setSelection(Integer.valueOf(settings.getCurriculumAccepted()),false);
        courseRejected.setSelection(Integer.valueOf(settings.getCurriculumDeclined()),false);
        courseOffered.setSelection(Integer.valueOf(settings.getCurriculumSuggested()),false);
        fullGroupSpinner.setSelection(Integer.valueOf(settings.getGroupIsFormed()),false);
        participantLeft.setSelection(Integer.valueOf(settings.getMemberLeft()),false);
        changeCreator.setSelection(Integer.valueOf(settings.getNewCreator()),false);
        newParticipant.setSelection(Integer.valueOf(settings.getNewMember()),false);
        teacherFind.setSelection(Integer.valueOf(settings.getTeacherFound()),false);

        getReview.setSelection(Integer.valueOf(settings.getReviewReceived()),false);
        yourInviteAccepted.setSelection(Integer.valueOf(settings.getInvitationAccepted()),false);
        yourInviteRejected.setSelection(Integer.valueOf(settings.getInvitationDeclined()),false);
        getNewInvite.setSelection(Integer.valueOf(settings.getInvitationReceived()),false);


        if (user.getRole().equals("Admin")) {
            sanctionsToAdmin.setSelection(Integer.valueOf(settings.getSanctionsAppliedToAdmin()),false);
            sanctionsToUser.setSelection(0);
            notSanctions.setSelection(Integer.valueOf(settings.getSanctionsCancelledToAdmin()),false);
            reportMessageSpinner.setSelection(Integer.valueOf(settings.getReportMessage()),false);
            sanctionsToUserCard.setVisibility(View.GONE);
        } else {
            sanctionsToAdmin.setSelection(0);
            sanctionsToUser.setSelection(Integer.valueOf(settings.getSanctionsAppliedToUser()),false);
            notSanctions.setSelection(Integer.valueOf(settings.getSanctionsCancelledToUser()),false);

            sanctionsToAdminCard.setVisibility(View.GONE);
            reportMessage.setVisibility(View.GONE);
        }

        listener();
        mainLayout.setVisibility(View.VISIBLE);

    }

    @Override
    public void showLoading() {

    }

    @Override
    public void stopLoading() {

    }

    @Override
    public void getError(Throwable error) {

    }

    private void click(Spinner spinner, String type) {
        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view,
                                       int position, long id) {
                ChangeSettingNotification model = new ChangeSettingNotification();
                switch (type) {
                    case "courseFinished": {
                        model.setConfiguringNotification("CourseFinished");
                        break;
                    }
                    case "courseAccepted": {
                        model.setConfiguringNotification("CurriculumAccepted");
                        break;
                    }
                    case "courseRejected": {
                        model.setConfiguringNotification("CurriculumDeclined");
                        break;
                    }
                    case "courseOffered": {
                        model.setConfiguringNotification("CurriculumSuggested");
                        break;
                    }
                    case "fullGroupSpinner": {
                        model.setConfiguringNotification("GroupIsFormed");
                        break;
                    }
                    case "participantLeft": {
                        model.setConfiguringNotification("MemberLeft");
                        break;
                    }
                    case "changeCreator": {
                        model.setConfiguringNotification("NewCreator");
                        break;
                    }
                    case "newParticipant": {
                        model.setConfiguringNotification("NewMember");
                        break;
                    }
                    case "getReview": {
                        model.setConfiguringNotification("ReviewReceived");
                        break;
                    }
                    case "yourInviteAccepted": {
                        model.setConfiguringNotification("InvitationAccepted");
                        break;
                    }
                    case "yourInviteRejected": {
                        model.setConfiguringNotification("InvitationDeclined");
                        break;
                    }
                    case "getNewInvite": {
                        model.setConfiguringNotification("InvitationReceived");
                        break;
                    }
                    case "reportMessageSpinner": {
                        model.setConfiguringNotification("ReportMessage");
                        break;
                    }
                    case "sanctionsToAdmin": {
                        model.setConfiguringNotification("SanctionsAppliedToAdmin");
                        break;
                    }
                    case "sanctionsToUser": {
                        model.setConfiguringNotification("SanctionsAppliedToUser");
                        break;
                    }
                    case "notSanctions": {
                        if((user.getRole().equals("Admin"))){
                            model.setConfiguringNotification("SanctionsCancelledToAdmin");
                        }
                        else{
                        model.setConfiguringNotification("SanctionsCancelledToUser");
                        }
                        break;
                    }
                    case "teacherFind":{
                        model.setConfiguringNotification("TeacherFound");
                        break;
                    }

                }
                switch (position){
                    case 0:{model.setNewValue("Default");
                        break;}
                    case 1:{model.setNewValue("DontSend");
                        break;}
                    case 2:{model.setNewValue("ToMail");
                        break;}
                    case 3:{model.setNewValue("OnSite");
                        break;}
                    case 4:{model.setNewValue("Everywhere");
                        break;}
                }
                notificationsSettingsPresenter.changeSettings(user.getToken(),model);
            }

            @Override
            public void onNothingSelected(AdapterView<?> arg0) {
            }
        });
    }
    private void listener(){
        click(courseFinished, "courseFinished");
        click(courseAccepted, "courseAccepted");
        click(courseRejected, "courseRejected");
        click(courseOffered, "courseOffered");
        click(fullGroupSpinner, "fullGroupSpinner");
        click(participantLeft, "participantLeft");
        click(changeCreator, "changeCreator");
        click(newParticipant, "newParticipant");
        click(teacherFind,"teacherFind");
        click(getReview, "getReview");
        click(yourInviteAccepted, "yourInviteAccepted");
        click(yourInviteRejected, "yourInviteRejected");
        click(getNewInvite, "getNewInvite");
        click(reportMessageSpinner, "reportMessageSpinner");
        click(sanctionsToAdmin, "sanctionsToAdmin");
        click(sanctionsToUser, "sanctionsToUser");
    }
}


