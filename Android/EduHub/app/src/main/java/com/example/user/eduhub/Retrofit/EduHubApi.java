package com.example.user.eduhub.Retrofit;

import com.example.user.eduhub.Models.CreateGroupModel;
import com.example.user.eduhub.Models.CreateGroupResponse;
import com.example.user.eduhub.Models.Group.GetGroupsModel;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.Group.Group_;
import com.example.user.eduhub.Models.Group.GroupsListObject;
import com.example.user.eduhub.Models.LoginModel;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Models.Registration.RegistrationModel;
import com.example.user.eduhub.Models.Registration.RegistrationResponseModel;


import java.util.List;

import io.reactivex.Observable;
import io.reactivex.Single;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.POST;
import retrofit2.http.Path;

/**
 * Created by user on 16.12.2017.
 */

public interface EduHubApi {
    @POST("/api/account/registration")
    Observable<RegistrationResponseModel> userRegistration(@Body RegistrationModel registrationModel);
    @POST("/api/account/login")
    Single<User> userLogin(@Body LoginModel loginModel);
    @GET("/api/group")
    Observable<GetGroupsModel> getGroups();
    @GET("/api/group/{idOfGroup}")
    Observable<Group> getInformationAbotGroup(@Path("idOfGroup")String id);
    @POST("/api/group/{groupId}/member/{inviterId}/invite/{invitedId}")
    Observable<String> invitedUser(@Header("Authorization") String token, @Path("groupId") String groupId, @Path("inviterId") String inviterId, @Path("invitedId") String invitedId);
    @GET("/api/user/profile/groups")
    Observable<GroupsListObject> getUsersGroup(@Header("Authorization") String token);
    @POST("/api/group")
    Single<CreateGroupResponse> createGroup(@Header("Authorization") String token, @Body CreateGroupModel model);

}
