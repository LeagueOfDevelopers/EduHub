import { createSelector } from 'reselect';

/**
 * Direct selector to the groupPage state domain
 */
const selectGroupPage = (state) => state.get('groupPage');

const makeSelectUsers = () => createSelector(
  selectGroupPage,
  (groupPageState) => groupPageState.get('users')
);

export {
  makeSelectUsers
};
