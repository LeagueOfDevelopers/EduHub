package com.example.user.eduhub.Adapters.PlaceHolder;

import android.content.Context;
import android.widget.TextView;

import com.example.user.eduhub.Models.UserProfile.Review;
import com.example.user.eduhub.R;
import com.mindorks.placeholderview.annotations.Layout;
import com.mindorks.placeholderview.annotations.Resolve;
import com.mindorks.placeholderview.annotations.View;
import com.mindorks.placeholderview.annotations.expand.ChildPosition;
import com.mindorks.placeholderview.annotations.expand.ParentPosition;

/**
 * Created by User on 31.01.2018.
 */
@Layout(R.layout.review_item)
public class ReviewItemsView {
    @ParentPosition
    private int mParentPosition;

    @ChildPosition
    private int mChildPosition;

    @View(R.id.reviewTitle)
    private TextView reviewTitle;

    @View(R.id.reviewText)
    private TextView reviewText;

    @View(R.id.author)
    private TextView author;

    @View(R.id.date)
    private TextView date;

    private Context context;
    private Review review;

    public ReviewItemsView(Context context, Review review) {
        this.context = context;
        this.review=review;

    }

    @Resolve
    private void onResolved() {
        reviewTitle.setText("Заголовок");
        reviewText.setText(review.getText());
        author.setText(review.getEvaluator());
        date.setText("2 декабря 2018");

    }
}
