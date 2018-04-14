import { takeEvery, call, put, select } from 'redux-saga/effects';
import {
  leaveGroupSuccess,
  leaveGroupFailed,
  enterGroupSuccess,
  enterGroupFailed,
  inviteMemberSuccess,
  inviteMemberFailed,
  editGroupTitleSuccess,
  editGroupTitleFailed,
  editGroupDescriptionSuccess,
  editGroupDescriptionFailed,
  editGroupTagsSuccess,
  editGroupTagsFailed,
  editGroupSizeSuccess,
  editGroupSizeFailed,
  editGroupPriceSuccess,
  editGroupPriceFailed,
  editGroupTypeSuccess,
  editGroupTypeFailed,
  editPrivacySuccess,
  editPrivacyFailed,
  searchInvitationMemberSuccess,
  searchInvitationMemberFailed,
  addPlanSuccess,
  addPlanFailed,
  acceptPlanFailed,
  acceptPlanSuccess,
  declinePlanFailed,
  declinePlanSuccess,
  getCurrentPlanFailed,
  getCurrentPlanSuccess,
  getCurrentChatSuccess,
  getCurrentChatFailed,
  sendMessageSuccess,
  sendMessageFailed,
  addTeacherReviewFailed,
  addTeacherReviewSuccess,
  getTagsSuccess,
  getTagsFailed
} from "./actions";
import {
  ENTER_GROUP_START,
  LEAVE_GROUP_START,
  INVITE_MEMBER_START,
  EDIT_GROUP_TITLE,
  EDIT_GROUP_DESCRIPTION,
  EDIT_GROUP_TAGS,
  EDIT_GROUP_SIZE,
  EDIT_GROUP_PRICE,
  EDIT_GROUP_TYPE,
  EDIT_GROUP_PRIVACY,
  SEARCH_INVITATION_MEMBER,
  ADD_PLAN_START,
  ACCEPT_PLAN_START,
  DECLINE_PLAN_START,
  GET_CURRENT_PLAN_START,
  GET_CURRENT_CHAT_START,
  SEND_MESSAGE_START,
  ADD_TEACHER_REVIEW_START,
  GET_GROUP_TAGS_START
} from "./constants";
import config from '../../config';

function* enterGroupSaga(action) {
  try {
    yield call(enterGroup, action.groupId, action.role);
    yield put(enterGroupSuccess());
  }
  catch(e) {
    yield put(enterGroupFailed(e))
  }
}

function* leaveGroupSaga(action) {
  try {
    yield call(leaveGroup, action.groupId, action.memberId, action.role);
    yield put(leaveGroupSuccess());
  }
  catch(e) {
    yield put(leaveGroupFailed(e))
  }
}

function* inviteMemberSaga(action) {
  try {
    yield call(inviteMember, action.groupId, action.invitedId, action.role);
    yield put(inviteMemberSuccess());
  }
  catch(e) {
    yield put(inviteMemberFailed(e))
  }
}

function inviteMember(groupId, invitedId, role) {
  if(role === 'Member') {
    return fetch(`${config.API_BASE_URL}/group/${groupId}/member/invitation`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json-patch+json',
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      },
      body: JSON.stringify({
        invitedId: invitedId
      })
    })
      .catch(error => error)
  }
  else if(role === 'Teacher') {
    return fetch(`${config.API_BASE_URL}/group/${groupId}/teacher/invitation`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json-patch+json',
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      },
      body: JSON.stringify({
        invitedId: invitedId
      })
    })
      .catch(error => error)
  }
}

function enterGroup(groupId, role) {
  if(role === 'Member') {
    return fetch(`${config.API_BASE_URL}/group/${groupId}/member`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json-patch+json',
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      }
    })
      .catch(error => error);
  }
  else if(role === 'Teacher') {
    return fetch(`${config.API_BASE_URL}/group/${groupId}/teacher`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json-patch+json',
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      }
    })
      .catch(error => error);
  }
}

function leaveGroup(groupId, memberId, role) {
  if(role === 'Member') {
    return fetch(`${config.API_BASE_URL}/group/${groupId}/member/${memberId}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json-patch+json',
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      }
    })
      .catch(error => error);
  }
  else if(role === 'Teacher') {
    return fetch(`${config.API_BASE_URL}/group/${groupId}/teacher`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json-patch+json',
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      }
    })
      .catch(error => error);
  }
}

function* editGroupTitleSaga(action) {
  try {
    yield call(editGroupTitle, action.id, action.title);
    yield put(editGroupTitleSuccess())
  }
  catch(e) {
    yield put(editGroupTitleFailed(e))
  }
}

function editGroupTitle(id, title) {
  return fetch(`${config.API_BASE_URL}/group/${id}/title`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      groupTitle: title
    })
  })
    .then(res => res.json())
    .catch(error => error)
}

function* editGroupTypeSaga(action) {
  try {
    yield call(editGroupType, action.id, action.groupType);
    yield put(editGroupTypeSuccess())
  }
  catch(e) {
    yield put(editGroupTypeFailed(e))
  }
}

function editGroupType(id, type) {
  return fetch(`${config.API_BASE_URL}/group/${id}/type`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      groupType: type
    })
  })
    .then(res => res.json())
    .catch(error => error)
}

function* editPrivacySaga(action) {
  try {
    yield call(editPrivacy, action.id, action.isPrivate);
    yield put(editPrivacySuccess())
  }
  catch(e) {
    yield put(editPrivacyFailed(e))
  }
}

function editPrivacy(id, isPrivate) {
  return fetch(`${config.API_BASE_URL}/group/${id}/privacy`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      isPrivate: isPrivate
    })
  })
    .then(res => res.json())
    .catch(error => error)
}

function* editGroupDescriptionSaga(action) {
  try {
    yield call(editGroupDescription, action.id, action.description);
    yield put(editGroupDescriptionSuccess())
  }
  catch(e) {
    yield put(editGroupDescriptionFailed(e))
  }
}

function editGroupDescription(id, description) {
  return fetch(`${config.API_BASE_URL}/group/${id}/description`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      groupDescription: description
    })
  })
    .then(res => res.json())
    .catch(error => error)
}

function* editGroupTagsSaga(action) {
  try {
    yield call(editGroupTags, action.id, action.tags);
    yield put(editGroupTagsSuccess())
  }
  catch(e) {
    yield put(editGroupTagsFailed(e))
  }
}

function editGroupTags(id, tags) {
  return fetch(`${config.API_BASE_URL}/group/${id}/tags`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      groupTags: tags
    })
  })
    .then(res => res.json())
    .catch(error => error)
}

function* editGroupSizeSaga(action) {
  try {
    yield call(editGroupSize, action.id, action.size);
    yield put(editGroupSizeSuccess())
  }
  catch(e) {
    yield put(editGroupSizeFailed(e))
  }
}

function editGroupSize(id, size) {
  return fetch(`${config.API_BASE_URL}/group/${id}/size`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      groupSize: size
    })
  })
    .then(res => res.json())
    .catch(error => error)
}

function* editGroupPriceSaga(action) {
  try {
    yield call(editGroupPrice, action.id, action.price);
    yield put(editGroupPriceSuccess())
  }
  catch(e) {
    yield put(editGroupPriceFailed(e))
  }
}

function editGroupPrice(id, price) {
  return fetch(`${config.API_BASE_URL}/group/${id}/price`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      groupPrice: price
    })
  })
    .then(res => res.json())
    .catch(error => error)
}

function* searchInvitationUsersSaga(action) {
  try {
    const data = yield call(getUsers, action.groupId, action.username);
    yield put(searchInvitationMemberSuccess(data.users));
  }
  catch(e) {
    yield put(searchInvitationMemberFailed(e))
  }
}

function getUsers(groupId, username) {
  return fetch(`${config.API_BASE_URL}/users/searchForInvitation`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json-patch+json'
    },
    body: JSON.stringify({
      groupId: groupId,
      username: username
    })
  })
    .then(res => res.json())
    .then(res => res)
    .catch(error => error)
}

function* addPlanSaga(action) {
  try {
    yield call(addPlan, action.groupId, action.plan);
    yield put(addPlanSuccess());
  }
  catch(e) {
    yield put(addPlanFailed(e))
  }
}

function addPlan(groupId, plan) {
  return fetch(`${config.API_BASE_URL}/group/${groupId}/course/curriculum`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      description: plan
    })
  })
    .catch(error => error)
}

function* acceptPlanSaga(action) {
  try {
    yield call(acceptPlan, action.groupId);
    yield put(acceptPlanSuccess());
  }
  catch(e) {
    yield put(acceptPlanFailed(e))
  }
}

function acceptPlan(groupId) {
  return fetch(`${config.API_BASE_URL}/group/${groupId}/course/curriculum`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    }
  })
    .catch(error => error)
}

function* declinePlanSaga(action) {
  try {
    yield call(declinePlan, action.groupId);
    yield put(declinePlanSuccess());
  }
  catch(e) {
    yield put(declinePlanFailed(e))
  }
}

function declinePlan(groupId) {
  return fetch(`${config.API_BASE_URL}/group/${groupId}/course/curriculum`, {
    method: 'DELETE',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      reason: ''
    })
  })
    .catch(error => error)
}

function* getCurrentPlanSaga(action) {
  try {
    const file = yield call(getCurrentPlan, action.filename);
    yield put(getCurrentPlanSuccess(file));
  }
  catch(e) {
    yield put(getCurrentPlanFailed(e))
  }
}

function getCurrentPlan(filename) {
  return fetch(`${config.API_BASE_URL}/file/${filename}`, {
    method: 'GET',
    headers: {
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    }
  })
    .then(res => res.json())
    .then(res => res)
    .catch(error => error)
}

function* getCurrentChatSaga(action) {
  try {
    const chat = yield call(getCurrentChat, action.groupId);
    yield put(getCurrentChatSuccess(chat));
  }
  catch(e) {
    yield put(getCurrentChatFailed(e))
  }
}

function getCurrentChat(groupId) {
  return fetch(`${config.API_BASE_URL}/group/${groupId}/chat`, {
    method: 'GET',
    headers: {
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    }
  })
    .then(res => res.json())
    .then(res => res)
    .catch(error => error)
}

function* sendMessageSaga(action) {
  try {
    const messageId = yield call(sendMessage, action.groupId, action.text);
    yield put(sendMessageSuccess(messageId));
  }
  catch(e) {
    yield put(sendMessageFailed(e))
  }
}

function sendMessage(groupId, text) {
  return fetch(`${config.API_BASE_URL}/group/${groupId}/chat`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      text: text
    })
  })
    .then(res => res.json())
    .then(res => res)
    .catch(error => error)
}

function* addTeacherReviewSaga(action) {
  try {
    yield call(addTeacherReview, action.groupId, action.title, action.text);
    yield put(addTeacherReviewSuccess());
  }
  catch(e) {
    yield put(addTeacherReviewFailed(e))
  }
}

function addTeacherReview(groupId, title, text) {
  return fetch(`${config.API_BASE_URL}/group/${groupId}/course/review`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      title: title,
      text: text
    })
  })
    .then(res => res)
    .catch(error => error)
}

function* getTagsSaga(action) {
  try {
    const data = yield call(getTags, action.tag);
    yield put(getTagsSuccess(data));
  }
  catch (e) {
    yield put(getTagsFailed(e))
  }
}

function getTags(tag) {
  return fetch(`${config.API_BASE_URL}/tags/search${tag ? `?tag=${tag}` : ''}`)
    .then(res => res.json())
    .then(res => res)
    .catch(error => error)
}

export default function* () {
  yield takeEvery(ENTER_GROUP_START, enterGroupSaga);
  yield takeEvery(LEAVE_GROUP_START, leaveGroupSaga);
  yield takeEvery(INVITE_MEMBER_START, inviteMemberSaga);
  yield takeEvery(EDIT_GROUP_TITLE, editGroupTitleSaga);
  yield takeEvery(EDIT_GROUP_DESCRIPTION, editGroupDescriptionSaga);
  yield takeEvery(EDIT_GROUP_TAGS, editGroupTagsSaga);
  yield takeEvery(EDIT_GROUP_SIZE, editGroupSizeSaga);
  yield takeEvery(EDIT_GROUP_PRICE, editGroupPriceSaga);
  yield takeEvery(EDIT_GROUP_PRIVACY, editPrivacySaga);
  yield takeEvery(EDIT_GROUP_TYPE, editGroupTypeSaga);
  yield takeEvery(SEARCH_INVITATION_MEMBER, searchInvitationUsersSaga);
  yield takeEvery(ADD_PLAN_START, addPlanSaga);
  yield takeEvery(ACCEPT_PLAN_START, acceptPlanSaga);
  yield takeEvery(DECLINE_PLAN_START, declinePlanSaga);
  yield takeEvery(GET_CURRENT_PLAN_START, getCurrentPlanSaga);
  yield takeEvery(GET_CURRENT_CHAT_START, getCurrentChatSaga);
  yield takeEvery(SEND_MESSAGE_START, sendMessageSaga);
  yield takeEvery(ADD_TEACHER_REVIEW_START, addTeacherReviewSaga);
  yield takeEvery(GET_GROUP_TAGS_START, getTagsSaga);
}
