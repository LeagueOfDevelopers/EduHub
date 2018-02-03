package com.example.user.eduhub.Adapters.PlaceHolder;

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
    @View(R.id.number_of_reviews)
    private TextView numberOfReviews;

    @Toggle(R.id.number_of_reviews)
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
        numberOfReviews.setText(heading);

    }

    @Expand
    private void onExpand(){

    }

    @Collapse
    private void onCollapse(){

    }
}
