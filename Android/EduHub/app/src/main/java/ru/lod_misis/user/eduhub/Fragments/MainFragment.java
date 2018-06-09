package ru.lod_misis.user.eduhub.Fragments;

import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.support.design.widget.TabLayout;
import android.support.v4.app.Fragment;
import android.support.v4.view.ViewPager;
import android.support.v7.widget.Toolbar;
import android.text.SpannableString;
import android.text.style.TypefaceSpan;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;


import ru.lod_misis.user.eduhub.Adapters.ViewPagerAdapter;
import ru.lod_misis.user.eduhub.CreateGroupActivity;

import com.example.user.eduhub.R;

/**
 * Created by User on 25.01.2018.
 */

public class MainFragment extends Fragment {
    ViewPager pager;
    ViewPagerAdapter adapter;
    Authorized_fragment authorized_fragment;
    AuthorizedFragmentForTeacher authorized_fragment2;


    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.main_fragment, null);
        Toolbar toolbar=getActivity().findViewById(R.id.toolbar);

        toolbar.setTitle("Полный курс");

        authorized_fragment=new Authorized_fragment();

        authorized_fragment2=new AuthorizedFragmentForTeacher();
        Button button=v.findViewById(R.id.create_group_btn);


        pager=v.findViewById(R.id.pager);
        adapter=new ViewPagerAdapter(getFragmentManager());
        adapter.addFragment(authorized_fragment,"Обучение");
        adapter.addFragment(authorized_fragment2,"Преподавание");

        pager.setAdapter(adapter);

        TabLayout tabLayout = (TabLayout) v.findViewById(R.id.tabs);
        tabLayout.setTabTextColors(Color.WHITE,Color.WHITE);
        tabLayout.setupWithViewPager(pager);
        button.setOnClickListener(click->{
            Intent intent=new Intent(getActivity(), CreateGroupActivity.class);
            startActivity(intent);
        });
        return v;
    }
}
