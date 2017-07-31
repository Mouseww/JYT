;(function(window) {

var svgSprite = '<svg>' +
  ''+
    '<symbol id="icon-suoyoudingdan" viewBox="0 0 1024 1024">'+
      ''+
      '<path d="M836.094888 64.760956 186.211541 64.760956c-31.907711 0-58.014298 26.106588-58.014298 58.014298l0 780.342608c0 31.908734 26.106588 58.014298 58.014298 58.014298l649.883347 0c31.908734 0 58.014298-26.106588 58.014298-58.014298l0-780.342608C894.109186 90.867544 868.002598 64.760956 836.094888 64.760956zM838.206992 867.157859c0 15.702634-12.847609 28.548196-28.54922 28.548196L212.648656 895.706055c-15.70161 0-28.548196-12.846586-28.548196-28.548196l0-708.424648c0-15.70161 12.846586-28.548196 28.548196-28.548196l597.009116 0c15.702634 0 28.54922 12.847609 28.54922 28.548196L838.206992 867.157859z"  ></path>'+
      ''+
      '<path d="M757.498832 205.458395 266.337439 205.458395c-6.78145 0-12.279674 5.498224-12.279674 12.279674l0 36.835953c0 6.78145 5.498224 12.279674 12.279674 12.279674l491.161393 0c6.782473 0 12.279674-5.498224 12.279674-12.279674l0-36.835953C769.778506 210.956619 764.281305 205.458395 757.498832 205.458395z"  ></path>'+
      ''+
      '<path d="M757.498832 335.077521 266.337439 335.077521c-6.78145 0-12.279674 5.498224-12.279674 12.279674l0 36.835953c0 6.78145 5.498224 12.279674 12.279674 12.279674l491.161393 0c6.782473 0 12.279674-5.498224 12.279674-12.279674l0-36.835953C769.778506 340.574722 764.281305 335.077521 757.498832 335.077521z"  ></path>'+
      ''+
      '<path d="M552.837592 464.696648 266.337439 464.696648c-6.78145 0-12.279674 5.498224-12.279674 12.279674l0 36.835953c0 6.782473 5.498224 12.279674 12.279674 12.279674l286.500153 0c6.782473 0 12.279674-5.497201 12.279674-12.279674L565.117266 476.976322C565.117266 470.193849 559.620066 464.696648 552.837592 464.696648z"  ></path>'+
      ''+
      '<path d="M552.837592 594.315774 266.337439 594.315774c-6.78145 0-12.279674 5.497201-12.279674 12.279674l0 36.83493c0 6.782473 5.498224 12.279674 12.279674 12.279674l286.500153 0c6.782473 0 12.279674-5.497201 12.279674-12.279674l0-36.83493C565.117266 599.812975 559.620066 594.315774 552.837592 594.315774z"  ></path>'+
      ''+
    '</symbol>'+
  ''+
    '<symbol id="icon-delete" viewBox="0 0 1024 1024">'+
      ''+
      '<path d="M854.357322 285.951661l-34.659381 663.552671-0.030699 0.378623c-2.548032 28.499078-26.759457 50.837852-55.115272 50.837852l-474.844775 0c-28.355815 0-52.577472-22.338774-55.125505-50.837852l-0.051165-0.757247-34.618449-663.174048c-0.583285-11.2973 8.084119-20.916379 19.371186-21.509896 11.2973-0.593518 20.916379 8.084119 21.509896 19.371186l34.58775 662.703327c0.798179 7.101745 7.41897 13.272281 14.326287 13.272281l474.844775 0c6.897084 0 13.517875-6.170536 14.316054-13.262048l34.608216-662.71356c0.593518-11.287067 10.212596-19.954471 21.509896-19.371186C846.273203 265.035282 854.940606 274.664593 854.357322 285.951661z"  ></path>'+
      ''+
      '<path d="M936.191118 141.716652c0 11.307533-9.15859 20.466124-20.466124 20.466124l-265.875416 0c-11.307533 0-20.466124-9.15859-20.466124-20.466124l0-61.35744c0-11.266601-9.168824-20.435425-20.435425-20.435425l-163.616428 0c-11.276834 0-20.445658 9.168824-20.445658 20.435425l0 61.35744c0 11.307533-9.15859 20.466124-20.466124 20.466124l-265.875416 0c-11.2973 0-20.466124-9.15859-20.466124-20.466124 0-11.307533 9.168824-20.466124 20.466124-20.466124l245.409292 0 0-40.891316c0-33.840736 27.53717-61.367673 61.377906-61.367673l163.616428 0c33.830503 0 61.367673 27.526937 61.367673 61.367673l0 40.891316 245.409292 0C927.032528 121.250528 936.191118 130.409119 936.191118 141.716652z"  ></path>'+
      ''+
      '<path d="M639.636982 305.33308l0 572.662615c0 11.2973-9.168824 20.466124-20.466124 20.466124-11.307533 0-20.466124-9.168824-20.466124-20.466124l0-572.662615c0-11.307533 9.15859-20.466124 20.466124-20.466124C630.468159 284.866956 639.636982 294.025547 639.636982 305.33308z"  ></path>'+
      ''+
      '<path d="M455.564663 305.33308l0 572.662615c0 11.2973-9.15859 20.466124-20.466124 20.466124-11.2973 0-20.466124-9.168824-20.466124-20.466124l0-572.662615c0-11.307533 9.168824-20.466124 20.466124-20.466124C446.406073 284.866956 455.564663 294.025547 455.564663 305.33308z"  ></path>'+
      ''+
    '</symbol>'+
  ''+
    '<symbol id="icon-kehu" viewBox="0 0 1024 1024">'+
      ''+
      '<path d="M250.939239 337.962222c32.027437 0 59.372226-11.32493 82.027202-33.992185 22.666232-22.660092 33.997302-50.011021 33.997302-82.033341 0-32.029484-11.33107-59.378365-33.997302-82.039481-22.654976-22.666232-49.999764-33.996279-82.027202-33.996279-32.034601 0-59.374272 11.330046-82.039481 33.996279-22.654976 22.662139-33.992185 50.004881-33.992185 82.039481 0 32.027437 11.337209 59.372226 33.992185 82.033341C191.56599 326.632175 218.904639 337.962222 250.939239 337.962222L250.939239 337.962222 250.939239 337.962222zM250.939239 337.962222"  ></path>'+
      ''+
      '<path d="M388.946406 519.0383c33.991162 33.987069 75.003228 50.985208 123.050524 50.985208 48.047296 0 89.064478-16.998139 123.056664-50.985208 33.991162-34.001395 50.984185-75.017554 50.984185-123.06178 0-48.042179-16.994046-89.053222-50.984185-123.050524-33.992185-33.996279-75.008344-50.988278-123.056664-50.988278-48.047296 0-89.060385 16.998139-123.050524 50.988278-33.991162 33.987069-50.990325 75.003228-50.990325 123.050524S354.955244 485.042022 388.946406 519.0383L388.946406 519.0383 388.946406 519.0383zM388.946406 519.0383"  ></path>'+
      ''+
      '<path d="M773.070994 337.962222c32.023344 0 59.367109-11.32493 82.034365-33.992185 22.654976-22.660092 33.979906-50.011021 33.979906-82.033341 0-32.029484-11.32493-59.378365-33.979906-82.039481-22.667256-22.666232-50.011021-33.996279-82.034365-33.996279-32.038694 0-59.378365 11.330046-82.043574 33.996279-22.650883 22.662139-33.980929 50.004881-33.980929 82.039481 0 32.027437 11.330046 59.372226 33.980929 82.033341C713.692628 326.632175 741.0323 337.962222 773.070994 337.962222L773.070994 337.962222 773.070994 337.962222zM773.070994 337.962222"  ></path>'+
      ''+
      '<path d="M890.900609 337.967338c-1.811252 0-8.378831 3.172249-19.708877 9.519818-11.32493 6.346545-26.066679 12.767791-44.190455 19.261693-18.124799 6.493901-36.106336 9.741875-53.931307 9.741875-20.250206 0-40.332591-3.469008-60.277852-10.426467 1.505283 11.177574 2.263553 21.150716 2.263553 29.912263 0 42.001603-12.240789 80.676437-36.722366 116.030643 48.952922 1.5104 88.985684 20.850887 120.113635 58.021461l60.736293 0c24.777313 0 45.627177-6.131651 62.541405-18.361183 16.920368-12.235672 25.382087-30.144554 25.382087-53.705156C947.111842 391.293871 928.381246 337.962222 890.900609 337.967338L890.900609 337.967338 890.900609 337.967338zM890.900609 337.967338"  ></path>'+
      ''+
      '<path d="M823.160809 704.404078c-3.174296-17.682731-7.18361-34.075073-12.014638-49.179073-4.836145-15.109116-11.335163-29.833469-19.482727-44.185338-8.162914-14.34573-17.530258-26.591635-28.112268-36.71725-10.577916-10.124592-23.49818-18.202571-38.749536-24.25031-15.261589-6.040576-32.112372-9.061376-50.537-9.061376-3.025916 0-9.513678 3.252067-19.491936 9.745968-9.973142 6.493901-20.99415 13.756305-33.081443 21.756513-12.088316 7.998161-28.245298 15.250332-48.500621 21.74935-20.255323 6.493901-40.648792 9.745968-61.188594 9.745968-20.539802 0-40.943504-3.252067-61.183478-9.745968-20.250206-6.499018-36.413328-13.751189-48.500621-21.74935-12.088316-8.000208-23.113417-15.262612-33.081443-21.756513-9.977235-6.493901-16.467043-9.745968-19.49296-9.745968-18.433838 0-35.274388 3.0208-50.535977 9.061376-15.261589 6.04774-28.175713 14.125719-38.754652 24.25031-10.578939 10.120498-19.940145 22.372543-28.102035 36.71725-8.157797 14.351869-14.650675 29.071106-19.487843 44.185338-4.831029 15.103999-8.836249 31.497365-12.009522 49.179073-3.179412 17.676591-5.28947 34.138518-6.346545 49.406247-1.058099 15.256472-1.590218 30.895661-1.590218 46.910403 0 36.253692 11.031241 64.887846 33.086559 85.880973 22.061458 20.998243 51.373041 31.497365 87.927585 31.497365L710.066031 918.099065c36.559661 0 65.862034-10.499122 87.933725-31.497365 22.055318-20.992103 33.081443-49.626257 33.081443-85.880973 0-16.014742-0.532119-31.648814-1.573845-46.910403C828.439022 738.543619 826.322825 722.081693 823.160809 704.404078L823.160809 704.404078 823.160809 704.404078zM823.160809 704.404078"  ></path>'+
      ''+
      '<path d="M345.660554 512.007163c-24.481577-35.354206-36.712133-74.02904-36.707017-116.030643 0-8.762571 0.757247-18.729573 2.263553-29.912263-19.940145 6.951319-40.038902 10.426467-60.277852 10.426467-17.831111 0-35.808554-3.247974-53.937446-9.741875-18.124799-6.493901-32.859385-12.914124-44.185338-19.261693-11.330046-6.346545-17.908882-9.519818-19.718087-9.519818-37.470403 0-56.205093 53.331649-56.205093 159.994947 0 23.560602 8.461719 41.469484 25.375947 53.705156 16.924461 12.230556 37.775348 18.356067 62.546521 18.356067l60.736293 0C256.679987 532.85805 296.707632 513.517563 345.660554 512.007163L345.660554 512.007163 345.660554 512.007163zM345.660554 512.007163"  ></path>'+
      ''+
    '</symbol>'+
  ''+
    '<symbol id="icon-ai-profit" viewBox="0 0 1024 1024">'+
      ''+
      '<path d="M710.630144 390.340768c-171.999262 0-311.377389 139.400077-311.377389 311.360428 0 171.948378 139.419033 311.348455 311.377389 311.348455 171.940397 0 311.339476-139.400077 311.339476-311.348455C1021.96962 529.740845 882.569543 390.340768 710.630144 390.340768zM780.109688 659.599818c5.22002 0.960795 10.220544 1.449673 14.621444 1.449673l47.299448 0c4.500671 0 8.439631 1.701096 11.719105 5.620102 3.32038 3.780324 4.920707 7.401015 4.920707 11.190318l0 33.998972c0 6.760485-4.761073 9.891299-13.840237 9.891299l-81.99981 0c-3.620691 0-7.08075 0.959797-10.460992 2.980161-3.279474 1.758963-4.739124 4.718172-4.739124 8.349837 0 3.948937 0.079817 7.730259 0.560713 11.96953 0.479899 4.280177 0.660484 7.900868 0.660484 10.540809 0 5.640056 4.879801 7.649445 13.980914 5.640056l80.700792 0c9.259749 0 13.840237 4.249248 13.840237 12.84951l0 31.270235c0 9.259749-6.74053 14.000868-19.62097 14.000868l-18.079508 0c-7.240383 0-14.719219 0.319267-22.759766 0.639532-7.720282 0.650507-15.120299 0.801161-21.559521 0.801161l-12.520266 0c-8.121361 0-12.380587 4.761073-12.380587 14.30916l0 46.791613 0 9.739648c0 4.899755-3.780324 7.230406-11.179343 7.230406L694.730633 898.86271c-11.980505 0-18.000688-5.151178-18.000688-15.511401 0-3.948937 0.340219-11.89969 0.740301-24.300231 0.620576-12.199003 0.300311-20.648611-0.679441-25.399708 0-4.660305-0.540759-7.790122-1.440694-9.239795-0.801161-1.359879-3.460059-2.019366-8.279997-2.019366l-83.360687 0c-12.03937 0-18.079508-6.1-18.079508-18.480587 0-2.659896-0.260402-6.270609-1.300016-10.689468-0.979751-4.099591-0.979751-8.449608 0-13.349363 1.040612-4.661302 2.419447-8.91953 4.20036-12.860485 1.919595-3.620691 5.540285-5.059389 11.000753-4.099591l16.640809 0 27.820153 0 27.659521 0 13.978918 0 6.920118 0c1.919595 0 2.719758-2.330651 2.719758-7.241381l0-26.840401c0-4.761073-5.540285-7.079752-16.481176-7.079752l-80.54016 0c-3.780324 0-6.919121-0.169611-9.719693-0.569693-2.900344-0.468924-4.20036-3.128819-4.20036-7.720282l0-17.278346c0-7.401015-0.459944-13.031094-1.380831-16.961074-1.898643-5.630079-0.459944-10.3702 4.040727-13.978918 4.679261-3.96091 10.760305-5.641054 18.079508-5.641054 7.479834 0.960795 15.600198 1.141381 24.51873 0.650507 8.680079-0.499853 15.519383-0.650507 20.100869-0.650507l16.480178 0c5.560239 0 7.480831-2.009388 5.560239-5.630079-1.779915-2.079228-7.3202-9.159978-16.481176-21.219302-9.419383-12.621035-19.059259-25.171232-29.180032-38.349987-12.079278-16.150934-26.060192-33.609865-41.740206-52.418699-6.439222-10.360223-5.059389-18.969465 4.20036-25.399708 4.560534-2.890367 9.400426-6.429245 14.459815-10.850099 5.139206-4.179408 11.000753-8.199183 17.519792-11.89969 9.159978-5.699919 17.918876-0.580667 26.279688 15.428591 2.900344 3.949935 8.13932 10.689468 16.080096 20.669563 7.880913 10.040956 16.000279 20.320365 24.201458 31.819973 8.199183 11.501604 16.158915 21.560519 23.638749 30.321412 7.480831 9.080161 11.499608 14.630423 12.620037 16.480178 1.760959 3.780324 5.141201 6.1 10.199592 7.08075 5.15916 0.959797 8.77985-0.650507 10.540809-4.2702 0.81912-2.010386 4.660305-7.791119 11.759013-17.840057 6.900164-9.72967 14.619448-20.74938 22.899446-33.129967 8.359814-12.290793 16.219776-23.720561 23.560928-34.810111 7.559651-10.770282 12.140138-17.690401 13.979916-20.430113 4.600442-6.660714 8.680079-10.840122 11.659242-12.699854 3.299428-1.910615 8.600262-1.349902 16.159913 1.380831 4.580488 1.83878 9.740645 4.659307 15.039484 8.349837 5.699919 3.86912 9.480243 6.768466 11.161385 8.690056 10.219546 10.459994 13.099936 19.379524 8.379768 26.780539-1.779915 2.979163-6.940073 10.039958-15.139255 21.379933-8.660125 11.250181-17.440973 23.219711-27.179623 36.10015-9.880325 12.699854-18.459635 24.599544-26.3615 36.10015-7.858964 11.319023-12.219955 18.089485-13.319432 19.768631C774.150365 655.650881 775.131114 658.47041 780.109688 659.599818zM384.571449 0.001596C172.792481 0.001596 1.172349 85.152176 1.172349 190.201089c0 105.029956 171.619134 190.188517 383.399099 190.188517 211.720102 0 383.358193-85.139605 383.358193-190.188517S596.290553 0.001596 384.571449 0.001596L384.571449 0.001596zM505.39017 429.190608c-38.019745 6.260632-78.558708 9.79951-120.819719 9.79951-182.47921 0-334.8585-63.288752-373.598591-147.979387C4.751136 304.591562 1.172349 318.71016 1.172349 333.220858c0 105.028959 171.619134 190.208472 383.399099 190.208472 10.300361 0 20.380227-0.37913 30.461092-0.780209C438.751106 485.871525 469.430697 454.051552 505.39017 429.190608L505.39017 429.190608zM757.409785 376.790865c6.639762-13.979916 10.479949-28.528527 10.479949-43.539078 0-14.539632-3.55983-28.629296-9.79951-42.20913-13.759422 30.109898-42.158247 57.459131-80.958201 80.279758 5.479425-0.269382 10.820168-0.850049 16.340499-0.850049C715.349313 370.471368 736.730244 372.731182 757.409785 376.790865L757.409785 376.790865zM384.571449 564.139899c-182.47921 0-334.8585-63.290748-373.598591-147.990362-6.220723 13.589811-9.79951 27.69943-9.79951 42.210128 0 102.049796 162.139889 185.08922 365.539088 189.748527 4.860844-29.560159 13.539926-57.809328 25.680064-84.160851C389.790471 563.990242 387.190438 564.139899 384.571449 564.139899L384.571449 564.139899zM362.771479 688.799804C190.111734 683.989843 48.172484 622.579779 10.972857 541.290339c-6.220723 13.599788-9.79951 27.69943-9.79951 42.220105 0 102.618491 163.979667 186.03904 369.018103 189.819365-5.099297-23.099986-7.980684-47.031064-7.980684-71.659541C362.210766 697.350181 362.590894 693.109913 362.771479 688.799804L362.771479 688.799804zM10.97186 666.451095C4.751136 680.040906 1.172349 694.13955 1.172349 708.650248c0 105.028959 171.619134 190.210467 383.399099 190.210467 14.259275 0 28.260143-0.449967 42.10038-1.210223-18.519498-25.22012-33.379394-53.290698-44.23947-83.279873C200.911947 813.869769 49.552317 750.800513 10.97186 666.451095L10.97186 666.451095zM459.990362 935.729102c-24.418958 2.429424-49.559261 3.839189-75.419911 3.839189-182.47921 0-334.8585-63.288752-373.598591-147.990362C4.751136 805.169735 1.172349 819.288333 1.172349 833.799031c0 105.039933 171.619134 190.199492 383.399099 190.199492 62.419747 0 121.179892-7.550671 173.279324-20.690515C521.11109 986.758988 488.089874 963.839588 459.990362 935.729102L459.990362 935.729102z"  ></path>'+
      ''+
    '</symbol>'+
  ''+
    '<symbol id="icon-sj-copy" viewBox="0 0 1024 1024">'+
      ''+
      '<path d="M854.575 561.648c-24.825 168.805-168.805 297.891-342.575 297.891-193.629 0-347.539-153.91-347.539-347.539s153.91-347.539 347.539-347.539c14.895 0 34.754 0 49.648 4.965v-99.297c-14.895-4.965-34.754-4.965-49.648-4.965-248.242 0-446.836 198.594-446.836 446.836s198.594 446.836 446.836 446.836 446.836-198.594 446.836-446.836c0-14.895 0-34.754-4.965-49.648h-193.629v99.297h94.332z"  ></path>'+
      ''+
      '<path d="M512 462.352v99.297c-109.227 0-198.594-64.543-198.594-148.945s89.367-148.945 198.594-148.945 198.594 64.543 198.594 148.945h-99.297c0-29.789-44.684-49.648-99.297-49.648v99.297z"  ></path>'+
      ''+
      '<path d="M512 660.945c-54.613 0-99.297-19.859-99.297-49.648h-99.297c0 84.402 89.367 148.945 198.594 148.945s198.594-64.543 198.594-148.945-89.367-148.945-198.594-148.945v198.594z"  ></path>'+
      ''+
    '</symbol>'+
  ''+
'</svg>'
var script = function() {
    var scripts = document.getElementsByTagName('script')
    return scripts[scripts.length - 1]
  }()
var shouldInjectCss = script.getAttribute("data-injectcss")

/**
 * document ready
 */
var ready = function(fn){
  if(document.addEventListener){
      document.addEventListener("DOMContentLoaded",function(){
          document.removeEventListener("DOMContentLoaded",arguments.callee,false)
          fn()
      },false)
  }else if(document.attachEvent){
     IEContentLoaded (window, fn)
  }

  function IEContentLoaded (w, fn) {
      var d = w.document, done = false,
      // only fire once
      init = function () {
          if (!done) {
              done = true
              fn()
          }
      }
      // polling for no errors
      ;(function () {
          try {
              // throws errors until after ondocumentready
              d.documentElement.doScroll('left')
          } catch (e) {
              setTimeout(arguments.callee, 50)
              return
          }
          // no errors, fire

          init()
      })()
      // trying to always fire before onload
      d.onreadystatechange = function() {
          if (d.readyState == 'complete') {
              d.onreadystatechange = null
              init()
          }
      }
  }
}

/**
 * Insert el before target
 *
 * @param {Element} el
 * @param {Element} target
 */

var before = function (el, target) {
  target.parentNode.insertBefore(el, target)
}

/**
 * Prepend el to target
 *
 * @param {Element} el
 * @param {Element} target
 */

var prepend = function (el, target) {
  if (target.firstChild) {
    before(el, target.firstChild)
  } else {
    target.appendChild(el)
  }
}

function appendSvg(){
  var div,svg

  div = document.createElement('div')
  div.innerHTML = svgSprite
  svg = div.getElementsByTagName('svg')[0]
  if (svg) {
    svg.setAttribute('aria-hidden', 'true')
    svg.style.position = 'absolute'
    svg.style.width = 0
    svg.style.height = 0
    svg.style.overflow = 'hidden'
    prepend(svg,document.body)
  }
}

if(shouldInjectCss && !window.__iconfont__svg__cssinject__){
  window.__iconfont__svg__cssinject__ = true
  try{
    document.write("<style>.svgfont {display: inline-block;width: 1em;height: 1em;fill: currentColor;vertical-align: -0.1em;font-size:16px;}</style>");
  }catch(e){
    console && console.log(e)
  }
}

ready(appendSvg)


})(window)
