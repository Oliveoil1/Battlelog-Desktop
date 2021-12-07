/**
 * Utility function to add CSS in multiple passes.
 * @param {string} styleString
 */
function addStyle(styleString) {
    const style = document.createElement('style');
    style.textContent = styleString;
    document.head.append(style);
}

window.addEventListener('DOMContentLoaded', (event) => {
    addStyle(`
        #community-bar {
            position: relative;
            background: rgba(8, 13, 16, 0.98);
            visibility: hidden;
        }

        .pull-right {
            float: right;
            visibility: visible;
        }

        #base-header .game-bar {
            position: relative;
            top: -50px;
        }

        #base-header-secondary-nav {
            display: none;
        }

        #content {
            top: -50px;
            position: relative;
        }
    `);
});