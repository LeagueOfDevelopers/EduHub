package ru.lod_misis.user.eduhub.Fragments;

import android.os.Bundle;
import android.support.design.widget.TabLayout;
import android.support.v4.app.Fragment;
import android.support.v4.view.ViewPager;
import android.support.v7.widget.Toolbar;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.example.user.eduhub.R;

import ru.lod_misis.user.eduhub.Adapters.ViewPagerAdapter;

/**
 * Created by User on 06.04.2018.
 */

public class CommonFragmentForNotifications extends Fragment {
    ViewPager pager;
    ViewPagerAdapter adapter;
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.main_notifications_fragment, null);
        Toolbar toolbar=getActivity().findViewById(R.id.toolbar);
        toolbar.setTitle("Главная");
        InvitationFragment invitationFragment=new InvitationFragment();

        NotificationsFragment notificationsFragment=new NotificationsFragment();



        pager=v.findViewById(R.id.pager);
        adapter=new ViewPagerAdapter(getFragmentManager());
        adapter.addFragment(notificationsFragment,"Уведомления");
        adapter.addFragment(invitationFragment,"Приглашения");

        pager.setAdapter(adapter);

        TabLayout tabLayout = (TabLayout) v.findViewById(R.id.tabs);
        tabLayout.setupWithViewPager(pager);
        return v;
    }
}
