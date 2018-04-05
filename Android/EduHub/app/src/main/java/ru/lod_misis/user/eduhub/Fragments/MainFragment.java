package ru.lod_misis.user.eduhub.Fragments;

import android.os.Bundle;
import android.support.design.widget.TabLayout;
import android.support.v4.app.Fragment;
import android.support.v4.view.ViewPager;
import android.support.v7.widget.Toolbar;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;


import ru.lod_misis.user.eduhub.Adapters.ViewPagerAdapter;

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
        toolbar.setTitle("Главная");
        authorized_fragment=new Authorized_fragment();

        authorized_fragment2=new AuthorizedFragmentForTeacher();



        pager=v.findViewById(R.id.pager);
        adapter=new ViewPagerAdapter(getFragmentManager());
        adapter.addFragment(authorized_fragment,"Обучение");
        adapter.addFragment(authorized_fragment2,"Преподавание");

        pager.setAdapter(adapter);

        TabLayout tabLayout = (TabLayout) v.findViewById(R.id.tabs);
        tabLayout.setupWithViewPager(pager);
        return v;
    }
}
