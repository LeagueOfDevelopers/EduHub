import { createSelector } from 'reselect';

/**
 * Direct selector to the header state domain
 */
const selectHeaderDomain = (state) => state.get('header');

const makeSelectUsers = () => createSelector(
  selectHeaderDomain,
  (headerState) => headerState.get('users')
);

export {
  selectHeaderDomain,
  makeSelectUsers
};
