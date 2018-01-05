import { createSelector } from 'reselect';

/**
 * Direct selector to the createGroupPage state domain
 */
const selectCreateGroupPage = (state) => state.get('createGroupPage');

/**
 * Other specific selectors
 */


/**
 * Default selector used by CreateGroupPage
 */
export {
  selectCreateGroupPage,
};
