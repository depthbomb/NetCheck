/* eslint-env node */
const colors = require('tailwindcss/colors');
const plugin = require('tailwindcss/plugin');

module.exports = {
	content: [
		'./index.html',
		'./src/**/*.{vue,js,ts,jsx,tsx}',
	],
	theme: {
		extend: {
			colors: {
				rose: colors.rose,
				pink: colors.pink,
				fuchsia: colors.fuchsia,
				purple: colors.purple,
				violet: colors.violet,
				indigo: colors.indigo,
				blue: colors.blue,
				sky: colors.sky,
				cyan: colors.cyan,
				teal: colors.teal,
				emerald: colors.emerald,
				green: colors.lime,
				lime: colors.lime,
				yellow: colors.yellow,
				amber: colors.amber,
				orange: colors.orange,
				red: colors.red,
				slate: colors.slate,
				gray: colors.gray,
				zinc: colors.zinc,
				neutral: colors.neutral,
				stone: colors.stone,
			},
			fontFamily: {
				mono: [
					'"Cascadia Code"',
					'ui-monospace',
					'SFMono-Regular',
					'Menlo',
					'Monaco',
					'Consolas',
					'"Liberation Mono"',
					'"Courier New"',
					'monospace'
				]
			},
			transitionProperty: {
				'button': 'color, background-color, box-shadow, opacity',
			},
			transitionTimingFunction: {
				'in-back': 'cubic-bezier(0.36, 0, 0.66, -0.56)',
				'out-back': 'cubic-bezier(0.34, 1.56, 0.64, 1)',
				'in-out-back': 'cubic-bezier(0.68, -0.6, 0.32, 1.6)',
			},
		},
	},
	variants: {
		extend: {
			backgroundColor: ['active', 'disabled'],
			borderColor: ['hover', 'active', 'focus', 'disabled'],
			borderWidth: ['hover', 'active', 'focus', 'disabled'],
			cursor: ['disabled'],
			opacity: ['active', 'disabled'],
			outline: ['hover', 'active', 'focus'],
			pointerEvents: ['hover', 'focus', 'disabled'],
			ringColor: ['hover', 'active', 'focus'],
			ringOffsetWidth: ['hover', 'active', 'focus'],
			textColor: ['hover', 'active', 'disabled']
		}
	},
};
