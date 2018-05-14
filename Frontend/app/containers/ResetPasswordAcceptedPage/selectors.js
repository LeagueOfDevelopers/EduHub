import { createSelector } from 'reselect';

/**
 * Direct selector to the resetPasswordAcceptedPage state domain
 */
const selectResetPasswordAcceptedPageDomain = (state) => state.get('resetPasswordAcceptedPage');

/**
 * Other specific selectors
 */


/**
 * Default selector used by ResetPasswordAcceptedPage
 */

const makeSelectResetPasswordAcceptedPage = () => createSelector(
  selectResetPasswordAcceptedPageDomain,
  (substate) => substate.toJS()
);

export default makeSelectResetPasswordAcceptedPage;
export {
  selectResetPasswordAcceptedPageDomain,
};
