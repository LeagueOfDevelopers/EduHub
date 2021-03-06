package ru.lod_misis.user.eduhub.Fragments;

import android.app.DownloadManager;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
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
import android.widget.ProgressBar;
import android.widget.RelativeLayout;
import android.widget.TextView;


import ru.lod_misis.user.eduhub.Adapters.TagsAdapter;
import ru.lod_misis.user.eduhub.AuthorizedUserActivity;
import ru.lod_misis.user.eduhub.Fakes.FakeExitFromGroupPresenter;
import ru.lod_misis.user.eduhub.Fakes.FakeGroupInformationPresenter;
import ru.lod_misis.user.eduhub.Fakes.FakesButton;
import ru.lod_misis.user.eduhub.Interfaces.View.ICourseMethodsView;
import ru.lod_misis.user.eduhub.Interfaces.View.IExitFromGroupView;
import ru.lod_misis.user.eduhub.Interfaces.View.IGroupView;
import ru.lod_misis.user.eduhub.Models.AddReviewModel;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Models.Group.Member;
import ru.lod_misis.user.eduhub.Models.SavedDataRepository;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Presenters.CourseMethodsPresenter;
import ru.lod_misis.user.eduhub.Presenters.ExitFromGroupPresenter;
import ru.lod_misis.user.eduhub.Presenters.GroupInformationPresenter;
import com.example.user.eduhub.R;
import ru.lod_misis.user.eduhub.Refactor_group;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

import java.util.ArrayList;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;

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

    FakesButton fakesButton=new FakesButton();
    GroupInformationPresenter groupInformationPresenter=new GroupInformationPresenter(this);
    FakeGroupInformationPresenter fakeGroupInformationPresenter=new FakeGroupInformationPresenter(this);
    ExitFromGroupPresenter exitFromGroupPresenter=new ExitFromGroupPresenter(this);
    FakeExitFromGroupPresenter fakeExitFromGroupPresenter=new FakeExitFromGroupPresenter(this);
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    CourseMethodsPresenter addCourseMethodsPresenter=new CourseMethodsPresenter(this);

    Button exit;
    User user;
    Group fullGroup;
    CardView resultCard;
    CardView voteCard;
    CardView linkCard;
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
    RelativeLayout mainLayout;
    DownloadManager downloadManager;
    SwipeRefreshLayout swipeContainer;
    ProgressBar progressBar;
    static Boolean isLoading;

    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        final View v = inflater.inflate(R.layout.group_information_fragment, null);
        sharedPreferences=getActivity().getSharedPreferences("User",MODE_PRIVATE);
        if(sharedPreferences.contains("ID")){
        user=savedDataRepository.loadSavedData(sharedPreferences);}
        isLoading=savedDataRepository.getLoadingProgress(group.getGroupInfo().getId(),sharedPreferences);
        Log.d("sdfsf","sgsfgsfg");
        resultCard=v.findViewById(R.id.result_card);
        voteCard=v.findViewById(R.id.vote_card);
        course=v.findViewById(R.id.course);
        result=v.findViewById(R.id.result);
        mainLayout=v.findViewById(R.id.main_layout);
        mainLayout.setVisibility(View.GONE);
        suggestion_course_card=v.findViewById(R.id.suggestion_course_card);
        reason_negative_response_card=v.findViewById(R.id.reason_negative_response_card);
        positive_response=v.findViewById(R.id.possitive_button_about_course);
        negative_response=v.findViewById(R.id.negative_button_about_course);
        progressBar=v.findViewById(R.id.progressBar);
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
        swipeContainer = (SwipeRefreshLayout) v.findViewById(R.id.swipeContainer);

        recyclerView=v.findViewById(R.id.tags);
        discription=v.findViewById(R.id.discription);
        linkCard=v.findViewById(R.id.link_card);
        downloadManager = (DownloadManager) getActivity().getSystemService(Context.DOWNLOAD_SERVICE);
        exit=v.findViewById(R.id.exit);
        refactorButton=v.findViewById(R.id.refactor_group_settings);
        load(user.getToken(),group.getGroupInfo().getId(),getContext());
        exit.setOnClickListener(click->{

            if(!fakesButton.getCheckButton()){
                for (Member member :fullGroup.getMembers()
                     ) {
                    if(member.getUserId().equals(user.getUserId())){
                        if(member.getRole()==3){
                            isTeacher=true;
                        }

                    }
                }
                if(isTeacher){
                    exitFromGroupPresenter.exitFromGroupForTeacher(user.getToken(),group.getGroupInfo().getId(),getContext());
                }
                else {
                exitFromGroupPresenter.exitFromGroupForUser(user.getToken(),group.getGroupInfo().getId(),user.getUserId(),getContext());}
            }else{
                fakeExitFromGroupPresenter.exitFromGroupForTeacher(user.getToken(),group.getGroupInfo().getId(),getContext());
            }
        });
        suggestion_course.setOnClickListener(click->{
            if(!fakesButton.getCheckButton()){
            Intent intent=new Intent(Intent.ACTION_GET_CONTENT);
            intent.setType("application/*");
            if (intent.resolveActivity(getActivity().getPackageManager()) != null) {
            getActivity().startActivityForResult(intent,1);
            isLoading=true;
            course.setVisibility(View.GONE);
            progressBar.setVisibility(View.VISIBLE);
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
               addCourseMethodsPresenter.positiveResponse(user.getToken(),group.getGroupInfo().getId(),getContext());}
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
                addCourseMethodsPresenter.negativeResponse(user.getToken(),group.getGroupInfo().getId(),reason_negative_response.getText().toString(),getContext());}
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
                EduHubApi eduHubApi= RetrofitBuilder.getApi(getContext());
                eduHubApi.addReview("Bearer "+user.getToken(),group.getGroupInfo().getId(),addReviewModel)
                        .subscribeOn(Schedulers.io())
                        .observeOn(AndroidSchedulers.mainThread())
                        .subscribe(()->{
                    add_review_card.setVisibility(View.GONE);
                        },throwable -> {Log.e("AddReview",throwable.toString());});
            }
        });
        closeCourse.setOnClickListener(click->{
            EduHubApi eduHubApi=RetrofitBuilder.getApi(getContext());
            eduHubApi.closeCourse("Bearer "+user.getToken(),group.getGroupInfo().getId())
                    .subscribeOn(Schedulers.io())
                    .observeOn(AndroidSchedulers.mainThread())
                    .subscribe(()->{
                RefreshData();
                            },
                            throwable -> {Log.e("CloseCourse",throwable.toString());});
        });
        link.setOnClickListener(click->{
            if(!link.getText().toString().equals("")&&fullGroup!=null){
                Uri uri=Uri.parse("http://85.143.104.47:2411/api/file/"+fullGroup.getGroupInfo().getCurriculum());
                DownloadManager.Request request = new DownloadManager.Request(uri);
                request.setAllowedNetworkTypes(DownloadManager.Request.NETWORK_WIFI | DownloadManager.Request.NETWORK_MOBILE);
                request.setAllowedOverRoaming(false);
                request.setTitle(fullGroup.getGroupInfo().getCurriculum());
                request.setDescription(fullGroup.getGroupInfo().getCurriculum());
                request.setVisibleInDownloadsUi(true);
                request.setDestinationInExternalPublicDir(Environment.DIRECTORY_DOWNLOADS, fullGroup.getGroupInfo().getCurriculum());


                downloadManager.enqueue(request);

            }
        });
        /*swipeContainer.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
            @Override
            public void onRefresh() {
                if(!fakesButton.getCheckButton()){
                    groupInformationPresenter.loadGroupInformation(group.getGroupInfo().getId(),getContext());}
            }
        });*/

        return v;

    }

    @Override
    public void onResume() {
        super.onResume();

    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);


    }


    @Override
    public void showLoading() {

    }

    @Override
    public void stopLoading() {

    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        savedDataRepository.saveLoadingProgress(group.getGroupInfo().getId(),isLoading,sharedPreferences);
    }

    @Override
    public void getError(Throwable error) {
        progressBar.setVisibility(View.GONE);
        course.setVisibility(View.VISIBLE);
        isLoading=false;
    }

    @Override
    public void getInformationAboutGroup(Group group) {
        if (flag) {

            exit.setVisibility(View.GONE);
            refactorButton.setVisibility(View.GONE);

        }
        fullGroup = group;
        Member member = new Member();
        course.setVisibility(View.VISIBLE);
        members.setText(group.getGroupInfo().getMemberAmount() + "/" + group.getGroupInfo().getSize());
        cost.setText("$" + group.getGroupInfo().getCost());
        if (user != null) {
            for (Member member1 : group.getMembers()) {
                if (user.getUserId().equals(member1.getUserId())) {
                    member = member1;
                    if (member.getRole() != 2) {
                        refactorButton.setVisibility(View.GONE);
                    }
                }
            }
        }
        recyclerView.setHasFixedSize(true);
        StaggeredGridLayoutManager staggeredGridLayoutManager = new StaggeredGridLayoutManager(1, StaggeredGridLayoutManager.HORIZONTAL);
        recyclerView.setLayoutManager(staggeredGridLayoutManager);
        TagsAdapter adapter = new TagsAdapter((ArrayList<String>) group.getGroupInfo().getTags());
        recyclerView.setAdapter(adapter);
        discription.setText(group.getGroupInfo().getDescription());

        if (!isLoading) {
            if (member.getUserId() != null) {
                switch (group.getGroupInfo().getCourseStatus()) {

                    case 0: {
                        progressBar.setVisibility(View.GONE);
                        closeCourseCard.setVisibility(View.GONE);
                        status.setText("Не предложено");
                        link.setVisibility(View.GONE);
                        linkCard.setVisibility(View.GONE);
                        refactorCourse.setVisibility(View.GONE);
                        add_review_card.setVisibility(View.GONE);

                        if (member.getRole() == 3) {
                            resultCard.setVisibility(View.GONE);
                            voteCard.setVisibility(View.GONE);
                            reason_negative_response_card.setVisibility(View.GONE);
                            suggestion_course_card.setVisibility(View.VISIBLE);
                        } else {
                            refactorCourse.setVisibility(View.GONE);
                            resultCard.setVisibility(View.GONE);
                            voteCard.setVisibility(View.GONE);
                            reason_negative_response_card.setVisibility(View.GONE);
                            suggestion_course_card.setVisibility(View.GONE);
                        }


                        break;
                    }

                    case 1: {
                        progressBar.setVisibility(View.GONE);
                        closeCourseCard.setVisibility(View.GONE);
                        result.setText(group.getGroupInfo().getVotersAmount().toString() + "/" + group.getGroupInfo().getMemberAmount());
                        refactorCourse.setVisibility(View.VISIBLE);
                        add_review_card.setVisibility(View.GONE);
                        linkCard.setVisibility(View.VISIBLE);
                        link.setText(group.getGroupInfo().getCurriculum());
                        status.setText("Идет голосование");

                        if (member.getRole() == 3) {

                            resultCard.setVisibility(View.VISIBLE);
                            voteCard.setVisibility(View.GONE);
                            reason_negative_response_card.setVisibility(View.GONE);
                            suggestion_course_card.setVisibility(View.GONE);
                        } else {
                            if (member.getCurriculumStatus().equals("2")|| member.getCurriculumStatus().equals("3")) {
                                resultCard.setVisibility(View.VISIBLE);
                                voteCard.setVisibility(View.GONE);
                                reason_negative_response_card.setVisibility(View.GONE);
                                suggestion_course_card.setVisibility(View.GONE);
                                refactorCourse.setVisibility(View.GONE);
                            } else {
                                resultCard.setVisibility(View.VISIBLE);
                                voteCard.setVisibility(View.VISIBLE);
                                reason_negative_response_card.setVisibility(View.GONE);
                                suggestion_course_card.setVisibility(View.GONE);
                            }
                        }


                        break;
                    }
                    case 2: {
                        progressBar.setVisibility(View.GONE);
                        closeCourseCard.setVisibility(View.GONE);
                        result.setText(group.getGroupInfo().getVotersAmount().toString() + "/" + group.getGroupInfo().getMemberAmount());
                        refactorCourse.setVisibility(View.VISIBLE);
                        add_review_card.setVisibility(View.GONE);
                        link.setText(group.getGroupInfo().getCurriculum());
                        linkCard.setVisibility(View.VISIBLE);
                        status.setText("Курс начат");

                        if (member.getRole() == 3) {

                            resultCard.setVisibility(View.GONE);
                            voteCard.setVisibility(View.GONE);
                            closeCourseCard.setVisibility(View.VISIBLE);
                            reason_negative_response_card.setVisibility(View.GONE);
                            suggestion_course_card.setVisibility(View.GONE);
                        } else {
                            if (member.getCurriculumStatus().equals("2")|| member.getCurriculumStatus().equals("3")) {
                                refactorCourse.setVisibility(View.GONE);
                                resultCard.setVisibility(View.GONE);
                                voteCard.setVisibility(View.GONE);
                                reason_negative_response_card.setVisibility(View.GONE);
                                suggestion_course_card.setVisibility(View.GONE);
                                closeCourseCard.setVisibility(View.GONE);
                            } else {
                                refactorCourse.setVisibility(View.GONE);
                                resultCard.setVisibility(View.GONE);
                                voteCard.setVisibility(View.GONE);
                                reason_negative_response_card.setVisibility(View.GONE);
                                suggestion_course_card.setVisibility(View.GONE);
                                closeCourseCard.setVisibility(View.GONE);
                            }


                        }
                        break;
                    }
                    case 3: {
                        progressBar.setVisibility(View.GONE);
                        closeCourseCard.setVisibility(View.GONE);
                        refactorCourse.setVisibility(View.GONE);
                        link.setText(group.getGroupInfo().getCurriculum());
                        status.setText("Закончен");
                        resultCard.setVisibility(View.GONE);
                        voteCard.setVisibility(View.GONE);
                        linkCard.setVisibility(View.VISIBLE);
                        reason_negative_response_card.setVisibility(View.GONE);
                        suggestion_course_card.setVisibility(View.GONE);


                        if (member.getRole() == 3) {

                            add_review_card.setVisibility(View.GONE);
                        } else {
                            add_review_card.setVisibility(View.VISIBLE);
                        }


                        break;
                    }
                }
            } else {
                closeCourseCard.setVisibility(View.GONE);
                status.setText("Вступи и узри");
                link.setText(group.getGroupInfo().getCurriculum());
                add_review_card.setVisibility(View.GONE);
                refactorCourse.setVisibility(View.GONE);
                resultCard.setVisibility(View.GONE);
                voteCard.setVisibility(View.GONE);
                linkCard.setVisibility(View.GONE);
                reason_negative_response_card.setVisibility(View.GONE);
                suggestion_course_card.setVisibility(View.GONE);
                progressBar.setVisibility(View.GONE);
            }


        }else {
            closeCourseCard.setVisibility(View.GONE);
            status.setText("Не предложено");
            link.setText(group.getGroupInfo().getCurriculum());
            add_review_card.setVisibility(View.GONE);
            refactorCourse.setVisibility(View.GONE);
            resultCard.setVisibility(View.GONE);
            voteCard.setVisibility(View.GONE);
            linkCard.setVisibility(View.GONE);
            reason_negative_response_card.setVisibility(View.GONE);
            suggestion_course_card.setVisibility(View.GONE);}
            mainLayout.setVisibility(View.VISIBLE);
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
        finishProgressBar();
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
            groupInformationPresenter.loadGroupInformation(user.getToken(),group.getGroupInfo().getId(),getContext());}
        else {

            fakeGroupInformationPresenter.loadGroupInformation(user.getToken(),group.getGroupInfo().getId(),getContext());
        }
    }
    public void finishProgressBar(){
        progressBar.setVisibility(View.GONE);
        course.setVisibility(View.VISIBLE);
        load(user.getToken(),group.getGroupInfo().getId(),getContext());
        isLoading=false;
    }
    public void load(String token,String id,Context context){
        if(!fakesButton.getCheckButton()){groupInformationPresenter.loadGroupInformation(token,id,context);}
        else {

            fakeGroupInformationPresenter.loadGroupInformation(token,id,context);
        }
    }

}
