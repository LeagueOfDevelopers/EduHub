package ru.lod_misis.user.eduhub.Fragments;

import android.content.Intent;
import android.os.Bundle;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.StaggeredGridLayoutManager;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

import ru.lod_misis.user.eduhub.Adapters.EmptyGroupAdapter;
import ru.lod_misis.user.eduhub.Adapters.GroupAdapter;
import ru.lod_misis.user.eduhub.Fakes.FakeGroupRepository;
import ru.lod_misis.user.eduhub.Fakes.FakesButton;
import ru.lod_misis.user.eduhub.Interfaces.View.IGroupListView;
import ru.lod_misis.user.eduhub.MainActivity;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Presenters.GroupsPresenter;
import com.example.user.eduhub.R;

import java.util.ArrayList;

/**
 * Created by user on 14.12.2017.
 */

public class TeacherFragment extends android.support.v4.app.Fragment implements IGroupListView {
    ArrayList<Group> groups;
    RecyclerView recyclerView;
    SwipeRefreshLayout swipeContainer;
    GroupsPresenter groupsPresenter=new GroupsPresenter(this);
    FakeGroupRepository fakeGroupRepository=new FakeGroupRepository(this);
    FakesButton fakesButton=new FakesButton();
    EmptyGroupAdapter emptyGroupAdapter=new EmptyGroupAdapter();

    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_for_users_and_teachers, null);
        Button btn=v.findViewById(R.id.btn);
        btn.setText("ЗАРЕГИСТРИРОВАТЬСЯ И НАЧАТЬ ПРЕПОДАВАТЬ");
        btn.setTextSize(10);
        recyclerView=v.findViewById(R.id.recycler);
        recyclerView.setHasFixedSize(true);
        StaggeredGridLayoutManager llm = new StaggeredGridLayoutManager(2,1);
        recyclerView.setLayoutManager(llm);
        swipeContainer = (SwipeRefreshLayout) v.findViewById(R.id.swipeContainer);
        if(!fakesButton.getCheckButton()){

            groupsPresenter.loadAllGroupsForTeachers(getContext());}else{
            fakeGroupRepository.loadAllGroupsForTeachers(getContext());
        }

        swipeContainer.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
            @Override
            public void onRefresh() {
                if(!fakesButton.getCheckButton()){
                    groupsPresenter.loadAllGroupsForTeachers(getContext());}
                    else{
                    fakeGroupRepository.loadAllGroupsForTeachers(getContext());
                }
            }
        });


        btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(getActivity(),MainActivity.class);
                startActivity(intent);
            }
        });
        return v;
    }

    public void setGroups(ArrayList<Group> groups){
        this.groups=groups;
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
    public void getGroups(ArrayList<Group> groups) {
        if(groups.size()==0){
            recyclerView.setAdapter(emptyGroupAdapter);
        }else{
            GroupAdapter adapter=new GroupAdapter(groups,getActivity(),getContext());
            adapter.setFlag(true);
            recyclerView.setAdapter(adapter);
        }
        swipeContainer.setRefreshing(false);
    }
}
