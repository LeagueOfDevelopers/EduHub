import { createSelector } from 'reselect';

const selectLogin = (state) => state.get('login');

const makeSelectCurrentUser = () => createSelector(
  selectLogin,
  (globalState) => globalState.get('currentUser').toJS()
);

const makeSelectIsExists = () => createSelector(
  selectLogin,
  (globalState) => globalState.get('isExists')
);

const makeSelectIsConfirmed = () => createSelector(
  selectLogin,
  (globalState) => globalState.get('isConfirmed')
);

export {
  makeSelectCurrentUser,
  makeSelectIsExists,
  makeSelectIsConfirmed
}
