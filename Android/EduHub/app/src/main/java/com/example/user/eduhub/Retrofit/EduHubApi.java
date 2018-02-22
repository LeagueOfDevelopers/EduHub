package com.example.user.eduhub.Retrofit;

import com.example.user.eduhub.Models.ChangeInvitationStatusModel;
import com.example.user.eduhub.Models.CreateGroupModel;
import com.example.user.eduhub.Models.CreateGroupResponse;
import com.example.user.eduhub.Models.Group.GetAllGroups;
import com.example.user.eduhub.Models.Group.GetGroupsModel;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.GroupChangeInviteStatusResponse;
import com.example.user.eduhub.Models.InvitationResponse;
import com.example.user.eduhub.Models.InviteUserModel;
import com.example.user.eduhub.Models.LoginModel;
import com.example.user.eduhub.Models.SearchModel;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Models.Registration.RegistrationModel;
import com.example.user.eduhub.Models.Registration.RegistrationResponseModel;
import com.example.user.eduhub.Models.UserProfile.RefactorUserRequestModel;
import com.example.user.eduhub.Models.UserProfile.UserProfileResponse;
import com.example.user.eduhub.Models.UserProfile.UserSearchProfileResponse;


import io.reactivex.Completable;
import io.reactivex.Observable;
import io.reactivex.Single;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.POST;
import retrofit2.http.PUT;
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
    Observable<GetAllGroups> getGroups();
    @GET(" /api/group/{groupId}")
    Observable<Group> getInformationAbotGroup(@Path("groupId")String id);
    @POST("/api/group/{groupId}/member/invitation")
    Single<String> invitedUser(@Header("Authorization") String token, @Path("groupId") String groupId, @Body InviteUserModel model);
    @GET("/api/user/profile/groups/{userId}")
    Observable<GetGroupsModel> getUsersGroup(@Header("Authorization") String token,@Path("userId") String userId);
    @POST("/api/group")
    Single<CreateGroupResponse> createGroup(@Header("Authorization") String token, @Body CreateGroupModel model);
    @GET("/api/user/profile/{userId}")
    Single<UserProfileResponse> getUsersProfile (@Header("Authorization") String token, @Path("userId") String userId);
    @POST("/api/users/search")
    Single<UserSearchProfileResponse> searchUser (@Body SearchModel model);
    @POST("/api/group/{groupId}/member")
    Completable signInUserToGroup (@Header("Authorization") String token, @Path("groupId") String groupId);
    @GET("/api/user/profile/invitations")
    Observable<InvitationResponse> getInvitations(@Header("Authorization") String token);
    @PUT("/api/user/profile/invitations")
    Single<GroupChangeInviteStatusResponse> changeStatusOfInvitation(@Header("Authorization") String token, @Body ChangeInvitationStatusModel model);
    @PUT("/api/user/profile/name")
    Single<String> changesUserName(@Header("Authorization") String token, @Body RefactorUserRequestModel model);
    @PUT("/api/user/profile/about")
    Single<String> changesAboutUser(@Header("Authorization") String token, @Body RefactorUserRequestModel model);
    @PUT("/api/user/profile/avatar")
    Single<String> changesUserAvatar(@Header("Authorization") String token, @Body RefactorUserRequestModel model);
    @PUT("/api/user/profile/contacts")
    Single<String> changesUsersContacts(@Header("Authorization") String token, @Body RefactorUserRequestModel model);
    @PUT("/api/user/profile/birthYear")
    Single<String> changesUsersBirthYear(@Header("Authorization") String token, @Body RefactorUserRequestModel model);
    @PUT("/api/user/profile/gender")
    Single<String> changesUsersGender(@Header("Authorization") String token, @Body Boolean sex);
    @DELETE("/api/group/{groupId}/member/{memberId}")
    Completable exitFromGroup(@Header("Authorization") String token,@Path("groupId") String groupId,@Path("memberId") String userId);
    @DELETE("/api/group/{groupId}/member/teacher/{memberId}")
    Completable exitFromGroupForTeacher(@Header("Authorization") String token,@Path("groupId") String groupId,@Path("memberId") String userId);

}
