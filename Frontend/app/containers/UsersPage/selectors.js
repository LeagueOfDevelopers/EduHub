import { createSelector } from 'reselect';

/**
 * Direct selector to the usersPage state domain
 */
const selectUsersPageDomain = (state) => state.get('usersPage');

/**
 * Other specific selectors
 */


/**
 * Default selector used by UsersPage
 */

const makeSelectUsers = () => createSelector(
  selectUsersPageDomain,
  (usersState) => usersState.get('users')
);


export {
  makeSelectUsers,
};
