package com.example.user.eduhub.Adapters.PlaceHolder;

import android.widget.TextView;

import com.example.user.eduhub.R;
import com.mindorks.placeholderview.annotations.Layout;
import com.mindorks.placeholderview.annotations.Resolve;
import com.mindorks.placeholderview.annotations.View;
import com.mindorks.placeholderview.annotations.expand.Collapse;
import com.mindorks.placeholderview.annotations.expand.Expand;
import com.mindorks.placeholderview.annotations.expand.Parent;
import com.mindorks.placeholderview.annotations.expand.ParentPosition;
import com.mindorks.placeholderview.annotations.expand.SingleTop;

/**
 * Created by User on 02.02.2018.
 */
@Parent
@SingleTop
@Layout(R.layout.empty_list)
public class EmptyInvitationsView {

    @View(R.id.empty_message)
    TextView emptyMessage;
    @ParentPosition
    private int mParentPosition;
    @Resolve
    private void onResolved() {
        emptyMessage.setText("Здесь будут уведомления");
    }
    @Expand
    private void onExpand(){

    }

    @Collapse
    private void onCollapse(){


    }

}
