import { createSelector } from 'reselect';

/**
 * Direct selector to the createGroupPage state domain
 */
const selectCreateGroupPageDomain = (state) => state.get('createGroupPage');

/**
 * Other specific selectors
 */


/**
 * Default selector used by CreateGroupPage
 */

const makeSelectCreateGroupPage = () => createSelector(
  selectCreateGroupPageDomain,
  (substate) => substate.toJS()
);

export default makeSelectCreateGroupPage;
export {
  selectCreateGroupPageDomain,
};
