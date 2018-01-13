package com.example.user.eduhub.Fragments;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

import com.example.user.eduhub.Adapters.GroupAdapter;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.Group.GroupInfo;
import com.example.user.eduhub.MainActivity;
import com.example.user.eduhub.R;

import java.util.ArrayList;

/**
 * Created by user on 14.12.2017.
 */

public class UserFragment extends android.support.v4.app.Fragment {
    ArrayList<Group> groups;


    RecyclerView recyclerView;
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_for_users_and_teachers, null);
        Button btn=getActivity().findViewById(R.id.btn);
        btn.setText("ЗАРЕГИСТРИРОВАТЬСЯ И НАЧАТЬ РАБОТУ");
        btn.setTextSize(10);
        recyclerView=v.findViewById(R.id.recycler);
        recyclerView.setHasFixedSize(true);
        LinearLayoutManager llm = new LinearLayoutManager(getActivity().getApplicationContext());
        recyclerView.setLayoutManager(llm);
        GroupAdapter adapter=new GroupAdapter(groups,getActivity());
        recyclerView.setAdapter(adapter);

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
}
