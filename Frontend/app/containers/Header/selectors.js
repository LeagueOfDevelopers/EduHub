import { createSelector } from 'reselect';

/**
 * Direct selector to the header state domain
 */
const selectHeaderDomain = (state) => state.get('header');

const makeSelectUsers = () => createSelector(
  selectHeaderDomain,
  (headerState) => headerState.get('users')
);

const makeSelectGroups = () => createSelector(
  selectHeaderDomain,
  (headerState) => headerState.get('groups')
);

const makeSelectPending = () => createSelector(
  selectHeaderDomain,
  (headerState) => headerState.get('pending')
);

export {
  selectHeaderDomain,
  makeSelectUsers,
  makeSelectGroups,
  makeSelectPending
};
