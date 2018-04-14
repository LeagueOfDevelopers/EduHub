import { createSelector } from 'reselect';

/**
 * Direct selector to the adminPage state domain
 */
const selectAdminPageDomain = (state) => state.get('adminPage');



const makeSelectUsers = () => createSelector(
  selectAdminPageDomain,
  (adminState) => adminState.get('users')
);

const makeSelectModerators = () => createSelector(
  selectAdminPageDomain,
  (adminState) => adminState.get('moderators')
);

const makeSelectReports = () => createSelector(
  selectAdminPageDomain,
  (adminState) => adminState.get('reports')
);

const makeSelectSanctions = () => createSelector(
  selectAdminPageDomain,
  (adminState) => adminState.get('sanctions')
);

const makeSelectHistory = () => createSelector(
  selectAdminPageDomain,
  (adminState) => adminState.get('adminHistory')
);

const makeSelectCurrentSanction = () => createSelector(
  selectAdminPageDomain,
  (adminState) => adminState.get('sanctions')
);

export {
  selectAdminPageDomain,
  makeSelectUsers,
  makeSelectHistory,
  makeSelectModerators,
  makeSelectReports,
  makeSelectSanctions,
  makeSelectCurrentSanction
};
