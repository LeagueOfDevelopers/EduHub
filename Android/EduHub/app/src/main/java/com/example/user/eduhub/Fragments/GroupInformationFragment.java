package com.example.user.eduhub.Fragments;

import android.content.Intent;
import android.content.SharedPreferences;
import android.net.Uri;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.StaggeredGridLayoutManager;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;


import com.example.user.eduhub.Adapters.TagsAdapter;
import com.example.user.eduhub.AuthorizedUserActivity;
import com.example.user.eduhub.Fakes.FakeExitFromGroupPresenter;
import com.example.user.eduhub.Fakes.FakeGroupInformationPresenter;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Interfaces.View.ICourseMethodsView;
import com.example.user.eduhub.Interfaces.View.IExitFromGroupView;
import com.example.user.eduhub.Interfaces.View.IFileRepositoryView;
import com.example.user.eduhub.Interfaces.View.IGroupView;
import com.example.user.eduhub.Models.AddFileResponseModel;
import com.example.user.eduhub.Models.AddReviewModel;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.Group.Member;
import com.example.user.eduhub.Models.SavedDataRepository;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Presenters.CourseMethodsPresenter;
import com.example.user.eduhub.Presenters.ExitFromGroupPresenter;
import com.example.user.eduhub.Presenters.FileRepository;
import com.example.user.eduhub.Presenters.GroupInformationPresenter;
import com.example.user.eduhub.R;
import com.example.user.eduhub.Refactor_group;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import java.io.File;
import java.util.ArrayList;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;

import static android.app.Activity.RESULT_OK;
import static android.content.Context.MODE_PRIVATE;


/**
 * Created by User on 05.01.2018.
 */

public class GroupInformationFragment extends Fragment implements IGroupView,IExitFromGroupView,ICourseMethodsView {
    private Group group;
    private Boolean flag=false;
    ImageView refactorButton;
    Uri uri;



    public void setGroup(Group group) {
        this.group = group;
    }
    Boolean isTeacher=false;
    TextView members;
    TextView cost;
    RecyclerView recyclerView;
    TextView discription;
    SwipeRefreshLayout swipeConteiner;
    FakesButton fakesButton=new FakesButton();
    GroupInformationPresenter groupInformationPresenter=new GroupInformationPresenter(this);
    FakeGroupInformationPresenter fakeGroupInformationPresenter=new FakeGroupInformationPresenter(this);
    ExitFromGroupPresenter exitFromGroupPresenter=new ExitFromGroupPresenter(this);
    FakeExitFromGroupPresenter fakeExitFromGroupPresenter=new FakeExitFromGroupPresenter(this);
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    CourseMethodsPresenter addCourseMethodsPresenter=new CourseMethodsPresenter(this);


    User user;
    CardView resultCard;
    CardView voteCard;
    CardView suggestion_course_card;
    CardView reason_negative_response_card;
    CardView add_review_card;
    CardView closeCourseCard;
    SharedPreferences sharedPreferences;
    Button positive_response;
    Button negative_response;
    Button suggestion_course;
    Button addReviewBtn;
    Button reason_negative_response_btn;
    Button closeCourse;
    ImageView refactorCourse;
    TextView link;
    TextView status;
    TextView result;
    EditText reason_negative_response;
    EditText addReview;
    CardView course;
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        final View v = inflater.inflate(R.layout.group_information_fragment, null);
        sharedPreferences=getActivity().getSharedPreferences("User",MODE_PRIVATE);
        if(sharedPreferences.contains("ID")){
        user=savedDataRepository.loadSavedData(sharedPreferences);}

        Log.d("sdfsf","sgsfgsfg");
        resultCard=v.findViewById(R.id.result_card);
        voteCard=v.findViewById(R.id.vote_card);
        course=v.findViewById(R.id.course);
        result=v.findViewById(R.id.result);
        suggestion_course_card=v.findViewById(R.id.suggestion_course_card);
        reason_negative_response_card=v.findViewById(R.id.reason_negative_response_card);
        positive_response=v.findViewById(R.id.possitive_button_about_course);
        negative_response=v.findViewById(R.id.negative_button_about_course);
        addReview=v.findViewById(R.id.add_review);
        add_review_card=v.findViewById(R.id.add_review_card);
        closeCourseCard=v.findViewById(R.id.close_course_card);
        addReviewBtn=v.findViewById(R.id.add_review_btn);
        closeCourse=v.findViewById(R.id.close_course);
        suggestion_course=v.findViewById(R.id.suggest_course);
        refactorCourse=v.findViewById(R.id.refactor_course);
        reason_negative_response_btn=v.findViewById(R.id.reason_negative_response_button);
        reason_negative_response=v.findViewById(R.id.reason_negative_response);
        link=v.findViewById(R.id.link);
        status=v.findViewById(R.id.status);
        members=v.findViewById(R.id.members);
        cost=v.findViewById(R.id.cost);
        recyclerView=v.findViewById(R.id.tags);
        discription=v.findViewById(R.id.discription);

        Button exit=v.findViewById(R.id.exit);
        refactorButton=v.findViewById(R.id.refactor_group_settings);
        if(flag){
            Button exitFromGroup=v.findViewById(R.id.exit);
            exitFromGroup.setVisibility(View.GONE);
            refactorButton.setVisibility(View.GONE);
            course.setVisibility(View.GONE);
        }
        exit.setOnClickListener(click->{
            if(!fakesButton.getCheckButton()){
                for (Member member :group.getMembers()
                     ) {
                    if(member.getUserId().equals(user.getUserId())){
                        if(member.getRole()==3){
                            isTeacher=true;
                        }

                    }
                }
                if(isTeacher){
                    exitFromGroupPresenter.exitFromGroupForTeacher(user.getToken(),group.getGroupInfo().getId());
                }
                else {
                exitFromGroupPresenter.exitFromGroupForUser(user.getToken(),group.getGroupInfo().getId(),user.getUserId());}
            }else{
                fakeExitFromGroupPresenter.exitFromGroupForTeacher(user.getToken(),group.getGroupInfo().getId());
            }
        });
        suggestion_course.setOnClickListener(click->{
            if(!fakesButton.getCheckButton()){
            Intent intent=new Intent(Intent.ACTION_GET_CONTENT);
            intent.setType("application/*");
            if (intent.resolveActivity(getActivity().getPackageManager()) != null) {
            getActivity().startActivityForResult(intent,1);

            }
        }else
        {group.getGroupInfo().setCourseStatus(1);
            resultCard.setVisibility(View.VISIBLE);
            voteCard.setVisibility(View.GONE);
            reason_negative_response_card.setVisibility(View.GONE);
            suggestion_course_card.setVisibility(View.GONE);}
        });

        refactorButton.setOnClickListener(click->{

            Intent intent=new Intent(getActivity(),Refactor_group.class);
            intent.putExtra("Group",group);
            getActivity().startActivity(intent);
        });
        positive_response.setOnClickListener(click->{
            if(!fakesButton.getCheckButton()){
               addCourseMethodsPresenter.positiveResponse(user.getToken(),group.getGroupInfo().getId());}
            else {
                getResponseAfterYourResponse();
            }
        });
        negative_response.setOnClickListener(click->{
            resultCard.setVisibility(View.VISIBLE);
            voteCard.setVisibility(View.VISIBLE);
            reason_negative_response_card.setVisibility(View.VISIBLE);
            suggestion_course_card.setVisibility(View.GONE);
        });
        reason_negative_response_btn.setOnClickListener(click->{
            if(!reason_negative_response.getText().toString().equals("")){
            if(!fakesButton.getCheckButton()){
                addCourseMethodsPresenter.negativeResponse(user.getToken(),group.getGroupInfo().getId(),reason_negative_response.getText().toString());}
            else {
                getResponseAfterYourResponse();
            }}else{

            }
        });
        refactorCourse.setOnClickListener(click->{
            Intent intent=new Intent(Intent.ACTION_GET_CONTENT);
            intent.setType("application/*");
            if (intent.resolveActivity(getActivity().getPackageManager()) != null) {
                getActivity().startActivityForResult(intent,1);

            }
        });
        addReviewBtn.setOnClickListener(click->{
            if(!addReview.getText().toString().equals("")){
                AddReviewModel addReviewModel=new AddReviewModel();
                addReviewModel.setText(addReview.getText().toString());
                addReviewModel.setTitle("Отзыв от "+user.getName());
                EduHubApi eduHubApi= RetrofitBuilder.getApi();
                eduHubApi.addReview("Bearer "+user.getToken(),group.getGroupInfo().getId(),addReviewModel)
                        .subscribeOn(Schedulers.io())
                        .observeOn(AndroidSchedulers.mainThread())
                        .subscribe(()->{
                    add_review_card.setVisibility(View.GONE);
                        },throwable -> {Log.e("AddReview",throwable.toString());});
            }
        });
        closeCourse.setOnClickListener(click->{
            EduHubApi eduHubApi=RetrofitBuilder.getApi();
            eduHubApi.closeCourse("Bearer "+user.getToken(),group.getGroupInfo().getId())
                    .subscribeOn(Schedulers.io())
                    .observeOn(AndroidSchedulers.mainThread())
                    .subscribe(()->{
                RefreshData();
                            },
                            throwable -> {Log.e("CloseCourse",throwable.toString());});
        });
        return v;
    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if(!fakesButton.getCheckButton()){
            groupInformationPresenter.loadGroupInformation(group.getGroupInfo().getId());}
        else {

            fakeGroupInformationPresenter.loadGroupInformation(group.getGroupInfo().getId());
        }
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

    @Override
    public void getInformationAboutGroup(Group group) {
        members.setText(group.getGroupInfo().getMemberAmount()+"/"+group.getGroupInfo().getSize());
        cost.setText("$"+group.getGroupInfo().getCost());

        recyclerView.setHasFixedSize(true);
        StaggeredGridLayoutManager staggeredGridLayoutManager=new StaggeredGridLayoutManager(1,StaggeredGridLayoutManager.HORIZONTAL);
        recyclerView.setLayoutManager(staggeredGridLayoutManager);
        TagsAdapter adapter=new TagsAdapter((ArrayList<String>) group.getGroupInfo().getTags());
        recyclerView.setAdapter(adapter);
        discription.setText(group.getGroupInfo().getDescription());

            if (user != null) {
                switch (group.getGroupInfo().getCourseStatus()) {

                    case 0: {
                        closeCourseCard.setVisibility(View.GONE);
                        status.setText("Не предложено");
                        link.setText(group.getGroupInfo().getCurriculum());
                        refactorCourse.setVisibility(View.GONE);
                        add_review_card.setVisibility(View.GONE);
                        for (Member member : group.getMembers()) {

                            if (user.getUserId().equals(member.getUserId())) {
                                if (member.getRole() == 3) {
                                    resultCard.setVisibility(View.GONE);
                                    voteCard.setVisibility(View.GONE);
                                    reason_negative_response_card.setVisibility(View.GONE);
                                    suggestion_course_card.setVisibility(View.VISIBLE);
                                } else {
                                    resultCard.setVisibility(View.GONE);
                                    voteCard.setVisibility(View.GONE);
                                    reason_negative_response_card.setVisibility(View.GONE);
                                    suggestion_course_card.setVisibility(View.GONE);
                                }
                            }
                        }

                        break;
                    }

                    case 1: {
                        closeCourseCard.setVisibility(View.GONE);
                        result.setText(group.getGroupInfo().getVotersAmount().toString()+"/"+group.getGroupInfo().getMemberAmount());
                        refactorCourse.setVisibility(View.VISIBLE);
                        add_review_card.setVisibility(View.GONE);
                        link.setText(group.getGroupInfo().getCurriculum());
                        status.setText("Идет голосование");
                        for (Member member : group.getMembers()) {
                            if (user.getUserId().equals(member.getUserId())) {
                                if (member.getRole() == 3) {

                                    resultCard.setVisibility(View.VISIBLE);
                                    voteCard.setVisibility(View.GONE);
                                    reason_negative_response_card.setVisibility(View.GONE);
                                    suggestion_course_card.setVisibility(View.GONE);
                                } else {
                                    if (member.getCurriculumStatus() == 2 || member.getCurriculumStatus() == 3) {
                                        resultCard.setVisibility(View.VISIBLE);
                                        voteCard.setVisibility(View.GONE);
                                        reason_negative_response_card.setVisibility(View.GONE);
                                        suggestion_course_card.setVisibility(View.GONE);
                                    } else {
                                        resultCard.setVisibility(View.VISIBLE);
                                        voteCard.setVisibility(View.VISIBLE);
                                        reason_negative_response_card.setVisibility(View.GONE);
                                        suggestion_course_card.setVisibility(View.GONE);
                                    }
                                }
                            }

                        }
                        break;
                    }
                    case 2:{closeCourseCard.setVisibility(View.GONE);
                        result.setText(group.getGroupInfo().getVotersAmount().toString()+"/"+group.getGroupInfo().getMemberAmount());
                        refactorCourse.setVisibility(View.VISIBLE);
                        add_review_card.setVisibility(View.GONE);
                        link.setText(group.getGroupInfo().getCurriculum());
                        status.setText("Курс начат");
                        for (Member member : group.getMembers()) {
                            if (user.getUserId().equals(member.getUserId())) {
                                if (member.getRole() == 3) {

                                    resultCard.setVisibility(View.GONE);
                                    voteCard.setVisibility(View.GONE);
                                    closeCourseCard.setVisibility(View.VISIBLE);
                                    reason_negative_response_card.setVisibility(View.GONE);
                                    suggestion_course_card.setVisibility(View.GONE);
                                } else {
                                    if (member.getCurriculumStatus() == 2 || member.getCurriculumStatus() == 3) {
                                        resultCard.setVisibility(View.GONE);
                                        voteCard.setVisibility(View.GONE);
                                        reason_negative_response_card.setVisibility(View.GONE);
                                        suggestion_course_card.setVisibility(View.GONE);
                                        closeCourseCard.setVisibility(View.GONE);
                                    } else {
                                        resultCard.setVisibility(View.GONE);
                                        voteCard.setVisibility(View.GONE);
                                        reason_negative_response_card.setVisibility(View.GONE);
                                        suggestion_course_card.setVisibility(View.GONE);
                                        closeCourseCard.setVisibility(View.GONE);
                                    }
                                }
                            }

                        }
                        break;}
                    case 3: {
                        closeCourseCard.setVisibility(View.GONE);
                        refactorCourse.setVisibility(View.GONE);
                        link.setText(group.getGroupInfo().getCurriculum());
                        status.setText("Закончен");
                        resultCard.setVisibility(View.GONE);
                        voteCard.setVisibility(View.GONE);
                        reason_negative_response_card.setVisibility(View.GONE);
                        suggestion_course_card.setVisibility(View.GONE);

                        for (Member member : group.getMembers()) {
                            if (user.getUserId().equals(member.getUserId())) {
                                if (member.getRole() == 3) {

                                    add_review_card.setVisibility(View.GONE);
                                } else {
                                    add_review_card.setVisibility(View.VISIBLE);
                                }
                            }
                        }

                        break;
                    }
                }
            } else {

            }





    }

    @Override
    public void getResponse() {
        Intent intent1 = new Intent(getActivity(),AuthorizedUserActivity.class);

        startActivity(intent1);
    }



    public void setFlag(Boolean flag) {
        this.flag = flag;
    }

    @Override
    public void getResponseAfterAddCourse() {

    }

    @Override
    public void getResponseAfterYourResponse() {
        resultCard.setVisibility(View.VISIBLE);
        voteCard.setVisibility(View.GONE);
        reason_negative_response_card.setVisibility(View.GONE);
        suggestion_course_card.setVisibility(View.GONE);
        Log.d("ZAEBALSA","12");
        RefreshData();
    }
    private void RefreshData(){
        if(!fakesButton.getCheckButton()){
            groupInformationPresenter.loadGroupInformation(group.getGroupInfo().getId());}
        else {

            fakeGroupInformationPresenter.loadGroupInformation(group.getGroupInfo().getId());
        }
    }


}
