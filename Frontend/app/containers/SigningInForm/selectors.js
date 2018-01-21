import { createSelector } from 'reselect';

const selectLogin = (state) => state.get('login');

const makeSelectCurrentUser = () => createSelector(
  selectLogin(),
  (globalState) => globalState.get('currentUser')
);

const makeSelectUsername = () => createSelector(
  makeSelectCurrentUser(),
  (currentUser) => currentUser.get('name')
);

const makeSelectAvatarLink = () => createSelector(
  makeSelectCurrentUser(),
  (currentUser) => currentUser.get('avatarLink')
);

const makeSelectToken = () => createSelector(
  makeSelectCurrentUser(),
  (currentUser) => currentUser.get('token')
);

export {
  makeSelectCurrentUser,
  makeSelectUsername,
  makeSelectAvatarLink,
  makeSelectToken
}
