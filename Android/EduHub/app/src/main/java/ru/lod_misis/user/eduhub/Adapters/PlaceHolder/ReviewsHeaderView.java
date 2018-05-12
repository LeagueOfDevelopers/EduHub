package ru.lod_misis.user.eduhub.Adapters.PlaceHolder;

import android.content.Context;
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
import com.mindorks.placeholderview.annotations.expand.Toggle;

/**
 * Created by User on 31.01.2018.
 */
@Parent
@SingleTop
@Layout(R.layout.review_header)
public class ReviewsHeaderView {
    @View(R.id.review_header)
    private TextView headerReviews;

    @Toggle(R.id.review_header)
    private TextView toggleView;

    @ParentPosition
    private int mParentPosition;

    private Context context;
    private String heading;

    public ReviewsHeaderView(Context mContext,String heading) {
        this.context = mContext;
        this.heading=heading;
    }
    @Resolve
    private void onResolved() {


    }

    @Expand
    private void onExpand(){

    }

    @Collapse
    private void onCollapse(){

    }
}
