package ru.lod_misis.user.eduhub.Fragments;

import android.app.DownloadManager;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.support.annotation.Nullable;
import android.support.design.widget.FloatingActionButton;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.widget.CardView;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;
import okhttp3.ResponseBody;
import ru.lod_misis.user.eduhub.Adapters.GroupMembersAdapter;
import ru.lod_misis.user.eduhub.Adapters.TagsAdapter;
import ru.lod_misis.user.eduhub.Classes.TypeOfEducation;
import ru.lod_misis.user.eduhub.Fakes.FakeGroupInformationPresenter;
import ru.lod_misis.user.eduhub.Fakes.FakesButton;
import ru.lod_misis.user.eduhub.Interfaces.IUpdateList;
import ru.lod_misis.user.eduhub.Interfaces.View.ICourseMethodsView;
import ru.lod_misis.user.eduhub.Interfaces.View.IFileRepositoryView;
import ru.lod_misis.user.eduhub.Interfaces.View.IGroupView;
import ru.lod_misis.user.eduhub.InviteUserToGroup;
import ru.lod_misis.user.eduhub.Models.AddFileResponseModel;
import ru.lod_misis.user.eduhub.Models.AddReviewModel;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Models.Group.Member;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Presenters.CourseMethodsPresenter;
import ru.lod_misis.user.eduhub.Presenters.FileRepository;
import ru.lod_misis.user.eduhub.Presenters.GroupInformationPresenter;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

import com.example.user.eduhub.R;
import com.xiaofeng.flowlayoutmanager.FlowLayoutManager;

import java.io.IOException;
import java.util.ArrayList;

/**
 * Created by User on 04.01.2018.
 */

public class GroupMembersFragment extends android.support.v4.app.Fragment implements IGroupView,IUpdateList,ICourseMethodsView ,IFileRepositoryView{
    private Group group;
    RecyclerView recyclerView;
    FrameLayout mainLayout;
   SwipeRefreshLayout swipeConteiner;
   User user;
   FloatingActionButton refactor;
   EditText review;
   CardView reviewCard;
   Button addReview;
   Button invite;
   Button positiveResponse;
   Button negativeResponse;
   TextView changeFile;
   TextView downloadFile;
   Button uploadFile;
   CardView voteCard;
    CardView uploadCourseCard;
    CardView inviteCard;
    LinearLayout buttonsForVote;
    Uri filesUri;
    FileRepository fileRepository;
    ProgressBar progressBar1;
    ProgressBar progressBar2;
    DownloadManager downloadManager;
    TextView groupName;
    TextView typeOfEducation;
    TextView cost;
    TextView aboutGroup;
    TextView participants;
    RecyclerView tags;

   FakesButton fakesButton=new FakesButton();
   GroupInformationPresenter groupInformationPresenter=new GroupInformationPresenter(this);
   FakeGroupInformationPresenter fakeGroupInformationPresenter=new FakeGroupInformationPresenter(this);
    CourseMethodsPresenter addCourseMethodsPresenter=new CourseMethodsPresenter(this);
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        View v = inflater.inflate(R.layout.group_members_fragment, null);
        downloadManager = (DownloadManager) getActivity().getSystemService(Context.DOWNLOAD_SERVICE);
        fileRepository=new FileRepository(this,getContext());
        addReview=v.findViewById(R.id.add_review);
        review=v.findViewById(R.id.review);
        reviewCard=v.findViewById(R.id.review_card);
        mainLayout=v.findViewById(R.id.main_layout);
        mainLayout.setVisibility(View.GONE);
        participants=v.findViewById(R.id.participants);
        progressBar2=v.findViewById(R.id.progressBar2);
        progressBar2.setVisibility(View.GONE);
        progressBar1=v.findViewById(R.id.progressBar1);
        progressBar1.setVisibility(View.GONE);
        recyclerView=v.findViewById(R.id.recycler);
        changeFile=v.findViewById(R.id.upload_course);
        downloadFile=v.findViewById(R.id.download_course);
        uploadFile=v.findViewById(R.id.upload_course_btn);
        buttonsForVote=v.findViewById(R.id.buttons_for_vote);
        refactor=v.findViewById(R.id.refactor_btn);
        invite=v.findViewById(R.id.invite_btn);
        swipeConteiner=v.findViewById(R.id.swipeConteinerForMembers);
        positiveResponse=v.findViewById(R.id.possitive_button_about_course);
        negativeResponse=v.findViewById(R.id.negative_button_about_course);
        groupName=v.findViewById(R.id.name_of_group);
        cost=v.findViewById(R.id.cost);
        typeOfEducation=v.findViewById(R.id.type_of_education);
        aboutGroup=v.findViewById(R.id.about_group);
        tags=v.findViewById(R.id.tagsRecycler);
        voteCard=v.findViewById(R.id.vote_card);
        inviteCard=v.findViewById(R.id.invite_card);
        uploadCourseCard=v.findViewById(R.id.upload_course_card);
        reviewCard.setVisibility(View.GONE);
        voteCard.setVisibility(View.GONE);
        inviteCard.setVisibility(View.GONE);
        uploadCourseCard.setVisibility(View.GONE);
        if(!fakesButton.getCheckButton()){}
        else {

            fakeGroupInformationPresenter.loadGroupInformation(user.getToken(),group.getGroupInfo().getId(),getContext());
        }
        swipeConteiner.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
            @Override
            public void onRefresh() {
                refreshData();
            }
        });
        invite.setOnClickListener(view->{
            Intent intent=new Intent(getActivity(),InviteUserToGroup.class);
            intent.putExtra("group",group);
            getActivity().startActivity(intent);
        });
        positiveResponse.setOnClickListener(click->{
            if(!fakesButton.getCheckButton()){
                addCourseMethodsPresenter.positiveResponse(user.getToken(),group.getGroupInfo().getId(),getContext());}
            else {
                getResponseAfterYourResponse();
            }
        });
        negativeResponse.setOnClickListener(click->{
            if(!fakesButton.getCheckButton()){
                addCourseMethodsPresenter.negativeResponse(user.getToken(),group.getGroupInfo().getId(),"",getContext());}
            else {
                getResponseAfterYourResponse();
            }

        });
        changeFile.setOnClickListener(click->{
            Intent intent=new Intent(Intent.ACTION_GET_CONTENT);
            intent.setType("application/*");
            if (intent.resolveActivity(getActivity().getPackageManager()) != null) {
                getActivity().startActivityForResult(intent,1);}
        });
        uploadFile.setOnClickListener(click->{
            if(filesUri!=null){
                fileRepository.loadFiletoServer(user.getToken(),filesUri);
                changeFile.setVisibility(View.GONE);
                progressBar2.setVisibility(View.VISIBLE);
            }else{
                if(changeFile.getText().toString().equals("Файл уже выбран")){
                    MakeToast("Новый файл не выбран");
                }else{
                    MakeToast("Выберите файл,который хотите загрузить");
                }
            }
        });
        addReview.setOnClickListener(click->{
            if(!review.getText().toString().equals("")){
                AddReviewModel addReviewModel=new AddReviewModel();
                addReviewModel.setText(addReview.getText().toString());
                addReviewModel.setTitle("Отзыв от "+user.getName());
                EduHubApi eduHubApi= RetrofitBuilder.getApi(getContext());
                eduHubApi.addReview("Bearer "+user.getToken(),group.getGroupInfo().getId(),addReviewModel)
                        .subscribeOn(Schedulers.io())
                        .observeOn(AndroidSchedulers.mainThread())
                        .subscribe(()->{
                            reviewCard.setVisibility(View.GONE);
                        },throwable -> {Log.e("AddReview",throwable.toString());});
            }
        });
        downloadFile.setOnClickListener(click->{
            if(!downloadFile.getText().toString().equals("")&&group!=null){
                Uri uri=Uri.parse("http://85.143.104.47:2411/api/file/"+group.getGroupInfo().getCurriculum());
                DownloadManager.Request request = new DownloadManager.Request(uri);
                request.setAllowedNetworkTypes(DownloadManager.Request.NETWORK_WIFI | DownloadManager.Request.NETWORK_MOBILE);
                request.setAllowedOverRoaming(false);
                request.setTitle(group.getGroupInfo().getCurriculum());
                request.setDescription(group.getGroupInfo().getCurriculum());
                request.setVisibleInDownloadsUi(true);
                request.setDestinationInExternalPublicDir(Environment.DIRECTORY_DOWNLOADS, group.getGroupInfo().getCurriculum());


                downloadManager.enqueue(request);
        }
        });
        return v;
    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if(!fakesButton.getCheckButton()){
            Log.d("GroupIdMembersFrag",group.getGroupInfo().getId()+"");
            groupInformationPresenter.loadGroupInformation(user.getToken(),group.getGroupInfo().getId(),getContext());}

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
        Member member = new Member();
        GroupMembersAdapter adapter;

        Log.d("GroupIdInMemberAdapter",this.group.getGroupInfo().getId());
        ArrayList<Member> members=(ArrayList<Member>) group.getMembers();
        if(user.getUserId()!=null){
         adapter=new GroupMembersAdapter(members,user,getActivity(),this.group,this,getContext());}else{
            adapter=new GroupMembersAdapter(members,getActivity(),this.group,this,getContext());
        }
        TagsAdapter tagsAdapter=new TagsAdapter((ArrayList<String>) group.getGroupInfo().getTags());
        recyclerView.setHasFixedSize(true);
        FlowLayoutManager flowLayoutManager=new FlowLayoutManager();
        LinearLayoutManager llm = new LinearLayoutManager(getActivity().getApplicationContext());
        recyclerView.setLayoutManager(llm);
        recyclerView.setAdapter(adapter);

        tags.setLayoutManager(new FlowLayoutManager());
        tags.setAdapter(tagsAdapter);
        groupName.setText(group.getGroupInfo().getTitle());
        participants.setText(group.getGroupInfo().getMemberAmount()+"/"+group.getGroupInfo().getSize());

        cost.setText(Double.valueOf(group.getGroupInfo().getCost().toString()).intValue()+"руб.");
        aboutGroup.setText(group.getGroupInfo().getDescription());
        switch (String.valueOf(group.getGroupInfo().getGroupType())){
            case "1":{typeOfEducation.setText(TypeOfEducation.Лекция.toString());break;}
            case "2":{typeOfEducation.setText(TypeOfEducation.Семинар.toString());break;}
            case "3":{typeOfEducation.setText(TypeOfEducation.МастерКласс.toString());break;}
        }

        swipeConteiner.setRefreshing(false);
        Log.d("sfsd","sdfsdf");
        if (user != null) {
            for (Member member1 : group.getMembers()) {
                if (user.getUserId().equals(member1.getUserId())) {
                    member = member1;
                    if (member.getRole() != 2) {
                        refactor.setVisibility(View.GONE);
                    }
                }
            }
        }
        if (member.getUserId() != null) {
            refactor.setVisibility(View.VISIBLE);
            if(group.getGroupInfo().getSize()==group.getGroupInfo().getCurrentAmount()||member.getRole() == 3){
                inviteCard.setVisibility(View.GONE);
            }else{
            inviteCard.setVisibility(View.VISIBLE);}
            switch (group.getGroupInfo().getCourseStatus()) {

                case 0: {
                    reviewCard.setVisibility(View.GONE);
                    voteCard.setVisibility(View.GONE);
                    if (member.getRole() == 3) {
                        uploadCourseCard.setVisibility(View.VISIBLE);
                    } else {
                        uploadCourseCard.setVisibility(View.GONE);

                    }


                    break;
                }

                case 1: {

                    reviewCard.setVisibility(View.GONE);
                    voteCard.setVisibility(View.VISIBLE);
                    changeFile.setText("Файл уже выбран");
                    if (member.getRole() == 3) {
                        uploadCourseCard.setVisibility(View.VISIBLE);
                        buttonsForVote.setVisibility(View.GONE);
                    } else {
                        uploadCourseCard.setVisibility(View.GONE);
                        if (member.getCurriculumStatus().equals("2")|| member.getCurriculumStatus().equals("3")) {
                            buttonsForVote.setVisibility(View.GONE);
                        } else {
                            buttonsForVote.setVisibility(View.VISIBLE);
                        }
                    }


                    break;
                }
                case 2: {
                    reviewCard.setVisibility(View.GONE);
                    voteCard.setVisibility(View.GONE);
                    uploadCourseCard.setVisibility(View.GONE);
                    inviteCard.setVisibility(View.GONE);


                    if (member.getRole() == 3) {


                    } else {
                        if (member.getCurriculumStatus().equals("2")|| member.getCurriculumStatus().equals("3")) {

                        } else {

                        }


                    }
                    break;
                }
                case 3: {

                    voteCard.setVisibility(View.GONE);
                    uploadCourseCard.setVisibility(View.GONE);
                    inviteCard.setVisibility(View.GONE);



                    if (member.getRole() == 3) {
                        reviewCard.setVisibility(View.GONE);

                    } else {
                        if(member.getCurriculumStatus().equals("2")||member.getCurriculumStatus().equals("3")){
                            reviewCard.setVisibility(View.GONE);
                        }else{
                            reviewCard.setVisibility(View.VISIBLE);
                        }
                    }


                    break;
                }
            }
        } else {
refactor.setVisibility(View.GONE);
        }
        mainLayout.setVisibility(View.VISIBLE);
        }

    public void setGroup(Group group) {
        this.group = group;
    }

    public void setUser(User user) {
        this.user = user;
    }

    @Override
    public void updateList() {
        if(!fakesButton.getCheckButton()){
            groupInformationPresenter.loadGroupInformation(user.getToken(),group.getGroupInfo().getId(),getContext());}
        else {

            fakeGroupInformationPresenter.loadGroupInformation(user.getToken(),group.getGroupInfo().getId(),getContext());
        }
    }
    public void load(String token,String id,Context context){

    }

    @Override
    public void getResponseAfterAddCourse() {
        refreshData();
        filesUri=null;
    }

    @Override
    public void getResponseAfterYourResponse() {
        buttonsForVote.setVisibility(View.GONE);
    }
    public void refreshDataAboutCourse(Uri uri){
     filesUri=uri;
     changeFile.setText("Файл выбран");
    }

    @Override
    public void getResponse(AddFileResponseModel addFileResponseModel) {
        addCourseMethodsPresenter.addPlan(user.getToken(),group.getGroupInfo().getId(),addFileResponseModel.getFileName(),getContext());
        changeFile.setVisibility(View.VISIBLE);
        progressBar2.setVisibility(View.GONE);
    }

    @Override
    public void getFile(ResponseBody file) throws IOException {

    }
    private void refreshData(){
        mainLayout.setVisibility(View.GONE);
        if(!fakesButton.getCheckButton()){
            groupInformationPresenter.loadGroupInformation(user.getToken(),group.getGroupInfo().getId(),getContext());}
        else {

            fakeGroupInformationPresenter.loadGroupInformation(user.getToken(),group.getGroupInfo().getId(),getContext());
        }
    }
    private void MakeToast(String s) {
        Toast toast = Toast.makeText(getActivity().getApplicationContext(),
                (s), Toast.LENGTH_LONG);
        toast.show();
    }

}