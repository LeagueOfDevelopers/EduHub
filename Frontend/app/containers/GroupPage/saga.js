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
  searchInvitationMemberFailed
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
  SEARCH_INVITATION_MEMBER
} from "./constants";
import config from '../../config';

function* enterGroupSaga(action) {
  try {
    yield call(enterGroup, action.groupId);
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
  return fetch(`${config.API_BASE_URL}/group/${groupId}/member/invitation`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      invitedId: invitedId,
      role: role
    })
  })
    .catch(error => error)
}

function enterGroup(groupId) {
  return fetch(`${config.API_BASE_URL}/group/${groupId}/member`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    }
  })
    .catch(error => error);
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
    return fetch(`${config.API_BASE_URL}/group/${groupId}/member/teacher/${memberId}`, {
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
}
