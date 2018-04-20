package ru.lod_misis.user.eduhub.Adapters.PlaceHolder;

import android.content.Context;
import android.util.Log;
import android.widget.TextView;

import ru.lod_misis.user.eduhub.Models.UserProfile.Review;
import com.example.user.eduhub.R;
import com.mindorks.placeholderview.annotations.Layout;
import com.mindorks.placeholderview.annotations.Resolve;
import com.mindorks.placeholderview.annotations.View;
import com.mindorks.placeholderview.annotations.expand.ChildPosition;
import com.mindorks.placeholderview.annotations.expand.ParentPosition;

import org.joda.time.DateTime;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.TimeZone;

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
        reviewTitle.setText(review.getTitle());
        reviewText.setText(review.getText());
        author.setText(review.getFromUser());
        Log.d("FromUser",review.getFromUser());
        Log.d("Date",review.getDate());
        SimpleDateFormat dateFormat=new SimpleDateFormat("yyyy-mm-dd'T'HH:mm:ss.SSSSSSZ");
        Log.d("newDate",dateFormat.format(new Date()).toString());
        DateTime dt = new DateTime( review.getDate() ) ;


        Long dateInt=dt.toDate().getTime()/1000/60/60;

        Log.d("Date",dateInt.toString());
        Long days;
        Long mes;
        if(new Date().getTime()/1000/60/60-dateInt==0){
            date.setText("<часа назад");
        }else{
            if(new Date().getTime()/1000/60/60-dateInt<24){
                date.setText(new Date().getTime()/1000/60/60-dateInt+"ч. назад");
            }
            if(new Date().getTime()/1000/60/60-dateInt>24){
                days=(new Date().getTime()/1000/60/60-dateInt)/24;
                if(days==1){
                    date.setText(days+" день назад");
                }else{
                    if(days<5) {
                        date.setText(days + " дня назад");
                    }else{
                        if(days>31){
                            mes=days/31;
                            if(mes==1){
                                date.setText("месяяц назад");
                            }else {
                                if(mes<5){
                                    date.setText(mes+" месяца назад");
                                }else{
                                    if(mes<12){
                                        date.setText(mes+" месяцев назад");
                                    }else{
                                        date.setText("больше года назад");
                                    }
                                }

                            }
                        }else{
                            date.setText(days + " дней назад");
                        }
                    }



                }


            }
        }


    }

}
