cmake_minimum_required(VERSION 4.0)

project(netcheck)

set(CMAKE_CXX_STANDARD 26)
set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTORCC ON)
set(CMAKE_AUTOUIC OFF)
set(CMAKE_PREFIX_PATH "C:/Qt/6.10.0/msvc2022_64/lib/cmake")
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_SOURCE_DIR}/bin)
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY_DEBUG ${CMAKE_SOURCE_DIR}/bin/Debug)
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY_MINSIZEREL ${CMAKE_SOURCE_DIR}/bin/MinSizeRel)

set(APP_RC "${CMAKE_CURRENT_SOURCE_DIR}/netcheck.rc")
set(RESOURCE_FILES "resources/icons.qrc")

find_package(Qt6 REQUIRED COMPONENTS
        Core
        Gui
        Widgets
        Network)

qt_standard_project_setup()

file(GLOB_RECURSE SOURCES CONFIGURE_DEPENDS src/*.cpp src/*.h)

add_executable(netcheck ${SOURCES} ${APP_RC} ${RESOURCE_FILES})

target_link_libraries(netcheck
        Qt::Core
        Qt::Gui
        Qt::Widgets
        Qt::Network
)

if (CMAKE_BUILD_TYPE MATCHES "MinSizeRel|Release")
    target_compile_definitions(${PROJECT_NAME} PUBLIC NOMINMAX WIN32_LEAN_AND_MEAN)

    set_target_properties(${PROJECT_NAME} PROPERTIES
            WIN32_EXECUTABLE TRUE
    )
endif ()
